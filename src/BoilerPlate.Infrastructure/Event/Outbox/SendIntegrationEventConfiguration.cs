using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlate.Infrastructure.Event.Outbox
{
    public class SendIntegrationEventConfiguration : IEntityTypeConfiguration<SendIntegrationEvent>
    {
        public void Configure(EntityTypeBuilder<SendIntegrationEvent> builder)
        {
            builder.ToTable("SendIntegrationEvent").HasKey(u => u.Id);

            builder.Property(u => u.Id).ValueGeneratedNever();

            builder.Property(u => u.TopicName).IsRequired().HasMaxLength(100);

            builder.Property(u => u.MessageHeader).IsRequired();

            builder.Property(u => u.MessageBody).IsRequired();

            builder.Property(u => u.OperationId).IsRequired();

            builder.Property(u => u.ActivityId).IsRequired();

            builder.Property(u => u.RetryCount).IsRequired().HasDefaultValue(0);

            builder.Property(u => u.IsSent).IsRequired().HasDefaultValue(false);

            builder.Property(u => u.CreatedAt).IsRequired();

            builder.Property(u => u.UpdatedAt).IsRequired();
        }
    }
}
