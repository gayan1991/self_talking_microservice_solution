using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlate.Service.DTO.Request
{
    public class UserContactRequesttDTO
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
    }
}
