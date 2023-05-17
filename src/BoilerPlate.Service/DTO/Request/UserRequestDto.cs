using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlate.Service.DTO.Request
{
    public class UserRequestDTO
    {
        public string Prefix { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Job { get; set; }
        public List<UserContactRequesttDTO> Contacts { get; set; }
    }
}
