using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlate.Infrastructure.Event.Outbox
{
    public class SendIntegrationEvent
    {
        public Guid Id { get; protected set; }
        public string TopicName { get; protected set; }
        public string MessageHeader { get; protected set; }
        public string MessageBody { get; protected set; }
        public string OperationId { get; protected set; }
        public string ActivityId { get; protected set; }
        public int RetryCount { get; protected set; } = 0;
        public bool IsSent { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public SendIntegrationEvent() { }

        public SendIntegrationEvent(string topic, string messageHeader, string messageBody, string operationId, string activityId)
        {
            Id = Guid.NewGuid();
            TopicName = topic;
            MessageHeader = messageHeader;
            MessageBody = messageBody;
            OperationId = operationId;
            ActivityId = activityId;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsSent()
        {
            IsSent = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void IncreaseRetryCount()
        {
            RetryCount++;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
