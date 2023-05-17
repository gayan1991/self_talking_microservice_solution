using BoilerPlate.Application.BackgroundServices;
using BoilerPlate.Infrastructure.Event;
using BoilerPlate.Infrastructure.Event.Outbox;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Interceptors
{
    public class UserSaveChangeInterceptor : SaveChangesInterceptor
    {
        private readonly KafkaTopics _topics;
        private readonly KafkaConfig _kafkaConfig;
        private readonly IServiceProvider _services;
        public UserSaveChangeInterceptor(IServiceProvider services, IOptionsMonitor<KafkaTopics> topics, IOptionsMonitor<KafkaConfig> kafkaConfig)
        {
            _services = services;
            _topics = topics.CurrentValue;
            _kafkaConfig = kafkaConfig.CurrentValue;
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            var config = new ProducerConfig { BootstrapServers = _kafkaConfig.Bootstrapservers };

            using (var scope = _services.CreateScope())
            {
                try
                {
                    var scopedProcessingService = scope.ServiceProvider.GetRequiredService<ISendIntegrationEventRepository>();

                    foreach (var entry in eventData.Context.ChangeTracker.Entries())
                    {
                        if (entry.Metadata.Name == typeof(SendIntegrationEvent).FullName)
                        {
                            var id = entry.Properties.Where(x => x.Metadata.Name == "Id").Select(x => Guid.Parse(x.CurrentValue.ToString())).FirstOrDefault();

                            var sendEvent = (scopedProcessingService.FindIntegrationEventById(id)).Result;

                            using (var producer = new ProducerBuilder<byte[], byte[]>(config).Build())
                            {
                                try
                                {
                                    var message = new Message<byte[], byte[]>
                                    {
                                        Value = Encoding.UTF8.GetBytes(new EventMessage(_topics.AuditTriggerTopic, sendEvent.MessageHeader, sendEvent.MessageBody).ToString()),
                                        Timestamp = new Timestamp(DateTime.UtcNow)
                                    };

                                    var ss = producer.ProduceAsync(_topics.AuditTriggerTopic, message);

                                    var ssw = ss.Result;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Oops, something went wrong: {e}");
                                }
                                finally
                                {
                                    producer.Dispose();
                                }
                            }
                        }
                    }
                }
                finally
                {
                    //scope.r();
                }
            }

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
               DbContextEventData eventData,
               InterceptionResult<int> result,
               CancellationToken cancellationToken = new CancellationToken())
        {
            using (var scope = _services.CreateScope())
            {
                try
                {
                    var scopedProcessingService = scope.ServiceProvider.GetRequiredService<ISendIntegrationEventRepository>();

                    var sendItegrationEvents = new List<SendIntegrationEvent>();

                    foreach (var entry in eventData.Context.ChangeTracker.Entries())
                    {
                        if (entry.Metadata.Name != typeof(SendIntegrationEvent).FullName)
                        {
                            var messageBody = entry.Properties.Aggregate("", (auditString, property) => auditString + $"{property.Metadata.Name}: '{property.CurrentValue}' ");

                            var integrationEvent = new SendIntegrationEvent(_topics.AuditTriggerTopic,
                                                                            $"{entry.Metadata.Name} - {entry.State}",
                                                                            messageBody,
                                                                            Activity.Current!.TraceId.ToString(),
                                                                            Activity.Current!.SpanId.ToString());

                            sendItegrationEvents.Add(integrationEvent);
                        }
                    }

                    if (sendItegrationEvents.Any())
                    {
                        scopedProcessingService.SaveIntegrationEvents(sendItegrationEvents);
                        scopedProcessingService.SaveChangesAsync().Wait();
                    }
                }
                catch
                {
                    //scope.r();
                }
            }
            return new ValueTask<InterceptionResult<int>>(result);
        }
    }
}
