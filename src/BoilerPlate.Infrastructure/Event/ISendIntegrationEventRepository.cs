using BoilerPlate.Infrastructure.Event.Outbox;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BoilerPlate.Infrastructure.Event
{
    public interface ISendIntegrationEventRepository
    {
        Task<SendIntegrationEvent> FindIntegrationEventById(Guid Id);
        Task<IEnumerable<SendIntegrationEvent>> GetIntegrationEvents(Expression<Func<SendIntegrationEvent, bool>> expression);
        void SaveIntegrationEvent (SendIntegrationEvent integrationEvent);
        void SaveIntegrationEvents(List<SendIntegrationEvent> integrationEvents);
        void UpdateIntegrationEvent(SendIntegrationEvent integrationEvent);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
