using BoilerPlate.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlate.Infrastructure.SeedHelper
{
    public static class UserSeedData
    {
        public static void UserSeedData1(this ModelBuilder builder)
        {
            var userId = Guid.NewGuid();
            var user = new
            {
                Id = userId,
                Prefix = UserPrefix.AdultMale,
                Name = "John",
                Address = string.Empty,
                Job = string.Empty
            };

            var userContact = new
            {
                Id = Guid.NewGuid(),
                Type = ContactType.Home,
                Number = "98756432",
                UserId = userId
            };

            builder.Entity<User>().HasData(user);
            builder.Entity<UserContact>().HasData(userContact);
        }
    }
}
