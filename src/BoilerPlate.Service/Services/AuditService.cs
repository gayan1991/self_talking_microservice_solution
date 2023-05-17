using BoilerPlate.Domain.Interfaces.Repositories;
using BoilerPlate.Domain.Models;
using BoilerPlate.Service.DTO.KAFKA;
using BoilerPlate.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Service.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;
        
        public AuditService(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        public async Task SaveAudit(AuditDto audit)
        {
            var aud = new Audit(audit.Tablename, audit.Record);
            _auditRepository.Save(aud);
            await _auditRepository.SaveChangesAsync();
        }
    }
}
