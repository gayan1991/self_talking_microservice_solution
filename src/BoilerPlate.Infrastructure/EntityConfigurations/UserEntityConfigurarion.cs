using BoilerPlate.Domain.Models;
using BoilerPlate.Domain.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlate.Infrastructure.EntityConfigurations
{
    public class UserEntityConfigurarion : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User").HasKey(u => u.Id);

            builder.Property(u => u.Id).ValueGeneratedNever();

            builder.Property(u => u.Prefix).IsRequired().HasMaxLength(10).HasConversion(to => to.ToString(),
                                                                                        from => Enumeration.FromDisplayName<UserPrefix>(from));

            builder.Property(u => u.Name).IsRequired();

            builder.Property(u => u.Address);

            builder.Property(u => u.Job);

            builder.HasMany(u => u.Contacts).WithOne(c => c.User).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
