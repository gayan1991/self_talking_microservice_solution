using BoilerPlate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoilerPlate.Domain.Interfaces.Repositories
{
    public interface IAuditRepository
    {
        void Save(Audit audit);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
