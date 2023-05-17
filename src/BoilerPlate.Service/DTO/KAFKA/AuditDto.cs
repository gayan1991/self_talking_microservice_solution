using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlate.Service.DTO.KAFKA
{
    public class AuditDto
    {
        public string Tablename { get; set; }
        public string Record { get; set; }
    }
}
