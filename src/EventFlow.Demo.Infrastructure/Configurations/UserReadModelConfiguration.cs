using EventFlow.Demo.Core.Users.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlow.Demo.Infrastructure.Configurations
{
    internal class UserReadModelConfiguration : IEntityTypeConfiguration<UserReadModel>
    {
        public void Configure(EntityTypeBuilder<UserReadModel> builder)
        {
            builder.HasIndex(e => e.Email).IsUnique();
        }
    }
}
