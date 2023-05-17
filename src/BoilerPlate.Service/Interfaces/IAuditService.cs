using BoilerPlate.Service.DTO.KAFKA;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Service.Interfaces
{
    public interface IAuditService
    {
        Task SaveAudit(AuditDto audit);
    }
}
