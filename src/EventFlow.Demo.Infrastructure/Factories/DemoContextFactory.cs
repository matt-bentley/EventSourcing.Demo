using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EventFlow.Demo.Infrastructure.Factories
{
    internal class DemoContextFactory : IDesignTimeDbContextFactory<DemoContext>
    {
        private IConfigurationRoot _configuration;

        public DemoContextFactory()
        {
            // this should be using a local db connection string which can go in source control
            // this factory should not get used anywhere apart from when the EF migrations are calculated
            var builder = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.ef.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public DemoContext CreateDbContext(string[] args)
        {
            var connectionString = _configuration["Sql:ConnectionString"];

            var options = new DbContextOptionsBuilder<DemoContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new DemoContext(options);
        }
    }
}
