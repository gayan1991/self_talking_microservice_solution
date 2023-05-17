using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Infrastructure.Event.Outbox
{
    public class SendIntegrationEventRepository : ISendIntegrationEventRepository
    {
        private readonly BoilerPlateDbContext _dbContext;

        public SendIntegrationEventRepository(BoilerPlateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SendIntegrationEvent> FindIntegrationEventById(Guid Id)
        {
            return await _dbContext.SendIntegrationEvents.Where(s => s.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<SendIntegrationEvent>> GetIntegrationEvents(Expression<Func<SendIntegrationEvent, bool>> expression)
        {
            return await _dbContext.SendIntegrationEvents.Where(expression).ToListAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void SaveIntegrationEvent(SendIntegrationEvent integrationEvent)
        {
            _dbContext.SendIntegrationEvents.Add(integrationEvent);
        }

        public void SaveIntegrationEvents(List<SendIntegrationEvent> integrationEvents)
        {
            _dbContext.SendIntegrationEvents.AddRange(integrationEvents);
        }

        public void UpdateIntegrationEvent(SendIntegrationEvent integrationEvent)
        {
            _dbContext.SendIntegrationEvents.Update(integrationEvent);
        }
    }
}
