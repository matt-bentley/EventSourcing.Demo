using EventFlow.Demo.Core.Applications.ReadModels;
using EventFlow.Demo.Core.Users.ReadModels;
using EventFlow.EntityFramework.EventStores;
using EventFlow.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EventFlow.Demo.Infrastructure
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {
        }

        public DbSet<ApplicationReadModel> Applications { get; set; }
        public DbSet<ComponentReadModel> Components { get; set; }


        public DbSet<UserReadModel> Users { get; set; }

        public DbSet<EventEntity> EventEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .AddEventFlowEvents();
            //.AddEventFlowSnapshots();
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
