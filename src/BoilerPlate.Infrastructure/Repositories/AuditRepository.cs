using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BoilerPlate.Domain.Interfaces.Repositories;
using BoilerPlate.Domain.Models;

namespace BoilerPlate.Infrastructure.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly AuditDbContext _dbContext;

        public AuditRepository(AuditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save(Audit audit)
        {
            _dbContext.Audit.Add(audit);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
