using BoilerPlate.Application.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoilerPlate.Application.BackgroundServices
{
    public class KafkaConsumerService : IHostedService
    {
        private readonly KafkaTopics _topics;
        private readonly KafkaConfig _kafkaConfig;
        private readonly IServiceProvider _serviceProvider;

        public KafkaConsumerService(IOptionsMonitor<KafkaTopics> topics, IOptionsMonitor<KafkaConfig> kafkaConfig, IServiceProvider serviceProvider)
        {
            _topics = topics.CurrentValue;
            _kafkaConfig = kafkaConfig.CurrentValue;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var scopedSvc = _serviceProvider.CreateScope();
            var scope = scopedSvc.ServiceProvider;

            var conf = new ConsumerConfig
            {
                GroupId = _kafkaConfig.Consumer.GroupID,
                BootstrapServers = _kafkaConfig.Bootstrapservers,
                AutoOffsetReset = Enum.Parse<AutoOffsetReset>(_kafkaConfig.Consumer.AutoOffSetReset)
            };

            using (var builder = new ConsumerBuilder<byte[], byte[]>(conf).Build())
            {
                builder.Subscribe(_topics.ToEnumerable());

                RunListener(builder, scope);
            }
            return Task.CompletedTask;
        }

        public async void RunListener(IConsumer<byte[], byte[]> builder, IServiceProvider scope)
        {
            var cancelToken = new CancellationTokenSource();
            try
            {
                while (true)
                {
                    var consumer = builder.Consume(cancelToken.Token);

                    var body = Encoding.UTF8.GetString(consumer.Message.Value);

                    var eve = JsonConvert.DeserializeObject<EventMessage>(body);

                    TriggerEvent(eve, scope);

                    Console.WriteLine($"Message: {consumer.Message.Value} received from {consumer.TopicPartitionOffset}");
                }
            }
            catch (Exception)
            {
                builder.Close();
            }
        }

        public async void TriggerEvent(EventMessage msg, IServiceProvider scope)
        {
            if (msg.TopicName == _topics.AuditTriggerTopic)
            {
                var eventHandler = scope.GetRequiredService<IEventHandler<EventMessage>>();
                await eventHandler.HandleAsync(msg);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
