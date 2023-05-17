using BoilerPlate.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlate.Infrastructure.EntityConfigurations
{
    public class UserContactEntityConfiguration : IEntityTypeConfiguration<UserContact>
    {
        public void Configure(EntityTypeBuilder<UserContact> builder)
        {
            builder.ToTable("UserContact").HasKey(u => u.Id);

            builder.Property(u => u.Id).ValueGeneratedNever();

            builder.Property(u => u.Type).IsRequired().HasMaxLength(100).HasConversion(to => to.ToString(),
                                                                                        from => (ContactType)Enum.Parse(typeof(ContactType), from));

            builder.Property(u => u.Number).IsRequired();
        }
    }
}
