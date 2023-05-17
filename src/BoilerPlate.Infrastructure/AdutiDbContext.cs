using BoilerPlate.Domain.Models;
using BoilerPlate.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlate.Infrastructure
{
    public class AuditDbContext : DbContext
    {
        public DbSet<Audit> Audit { get; set; }

        public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuditEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
