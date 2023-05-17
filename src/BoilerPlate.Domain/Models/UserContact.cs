using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlate.Domain.Models
{
    public class UserContact
    {
        public Guid Id { get; set; }
        public ContactType Type { get; set; }
        public string Number { get; set; }
        public User User { get; set; }

        public UserContact() { }

        public UserContact(ContactType type, string number, User user)
        {
            Id = Guid.NewGuid();
            Type = type;
            Number = number;
            User = user;
        }
    }
}
