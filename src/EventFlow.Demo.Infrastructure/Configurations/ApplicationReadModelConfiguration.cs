using EventFlow.Demo.Core.Applications.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlow.Demo.Infrastructure.Configurations
{
    internal class ApplicationReadModelConfiguration : IEntityTypeConfiguration<ApplicationReadModel>
    {
        public void Configure(EntityTypeBuilder<ApplicationReadModel> builder)
        {
            builder.Navigation(e => e.Components).AutoInclude();

            builder.HasMany(e => e.Components)
                   .WithOne()
                   .HasForeignKey(e => e.ApplicationId);
        }
    }
}
