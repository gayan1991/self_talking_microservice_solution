using BoilerPlate.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlate.Domain.Models
{
    public enum ContactType
    {
        Mobile,
        Home
    }

    public class UserPrefix : Enumeration
    {
        public static readonly UserPrefix AdultMale = new UserPrefix(1, "Mr.");
        public static readonly UserPrefix AdolescentMale = new UserPrefix(2, "Master");
        public static readonly UserPrefix MarriedFemale = new UserPrefix(3, "Mrs.");
        public static readonly UserPrefix SingleFemale = new UserPrefix(4, "Ms.");

        public UserPrefix(short id, string name) : base(id, name)
        { }
    }
}
