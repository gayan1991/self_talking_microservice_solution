using System;
using System.Collections.Generic;
using System.Linq;

namespace BoilerPlate.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public UserPrefix Prefix { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Job { get; set; }

        private readonly List<UserContact> _userContact = new List<UserContact>();
        public IReadOnlyList<UserContact> Contacts => _userContact;

        public User() { }

        public User(UserPrefix prefix, string name, string address, string job)
        {
            Id = Guid.NewGuid();
            Prefix = prefix;
            Name = name;
            Address = address;
            Job = job;
        }

        public void AddOrUpdateContact(ContactType type, string number, Guid id = default)
        {
            if (IsNumberValid(number))
            {
                var contact = _userContact.Where(c => c.Id == id).FirstOrDefault();

                if (contact is null)
                {
                    _userContact.Add(new UserContact(type, number, this));
                }
                else
                {
                    contact.Number = number;
                    contact.Type = type;
                }
            }
        }

        private bool IsNumberValid(string number)
        {
            foreach (char c in number.ToCharArray())
            {
                if (char.IsLetter(c))
                {
                    throw new InvalidOperationException("Contact number cannot contain any letters");
                }
            }
            return true;
        }
    }
}
