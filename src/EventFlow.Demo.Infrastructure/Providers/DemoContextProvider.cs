using EventFlow.Demo.Infrastructure.Settings;
using EventFlow.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EventFlow.Demo.Infrastructure.Providers
{
    public class DemoContextProvider : IDbContextProvider<DemoContext>, IDisposable
    {
        private readonly DbContextOptions<DemoContext> _options;

        public DemoContextProvider(IOptions<SqlSettings> settings)
        {
            _options = new DbContextOptionsBuilder<DemoContext>()
                .UseSqlServer(settings.Value.ConnectionString)
                .Options;
        }

        public DemoContext CreateContext()
        {
            return new DemoContext(_options);
        }

        public void Dispose()
        {
        }
    }
}
