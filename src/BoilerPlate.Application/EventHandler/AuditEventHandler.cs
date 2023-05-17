using BoilerPlate.Application.BackgroundServices;
using BoilerPlate.Application.Interfaces;
using BoilerPlate.Service.DTO.KAFKA;
using BoilerPlate.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Application.EventHandler
{
    public class AuditEventHandler : IEventHandler<EventMessage>
    {
        private readonly IAuditService _auditService;

        public AuditEventHandler(IAuditService auditService)
        {
            _auditService = auditService;
        }

        public Task HandleAsync(EventMessage eventMessage)
        {
            var dto = new AuditDto
            {
                Tablename = eventMessage.TableName,
                Record = eventMessage.Body
            };

            _auditService.SaveAudit(dto);

            return Task.FromResult(true);
        }
    }
}
