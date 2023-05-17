using BoilerPlate.Domain.Models;
using Microsoft.EntityFrameworkCore;
using BoilerPlate.Infrastructure.EntityConfigurations;
using BoilerPlate.Infrastructure.SeedHelper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BoilerPlate.Infrastructure.Event.Outbox;

namespace BoilerPlate.Infrastructure
{
    public class BoilerPlateDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserContact> UserContacts { get; set; }
        public DbSet<SendIntegrationEvent> SendIntegrationEvents { get; set; }

        public BoilerPlateDbContext(DbContextOptions<BoilerPlateDbContext> options) : base (options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UserSeedData1();

            modelBuilder.ApplyConfiguration(new SendIntegrationEventConfiguration());
            modelBuilder.ApplyConfiguration(new UserContactEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfigurarion());
            base.OnModelCreating(modelBuilder);
        }

        public override EntityEntry Add(object entity)
        {
            return base.Add(entity);
        }

        public override EntityEntry Update(object entity)
        {
            return base.Update(entity);
        }
    }
}
