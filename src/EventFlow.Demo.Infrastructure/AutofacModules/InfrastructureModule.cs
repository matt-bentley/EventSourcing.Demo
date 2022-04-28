using Autofac;
using EventFlow.Demo.Core.Abstractions.Repositories;
using EventFlow.Demo.Infrastructure.Repositories;
using EventFlow.Demo.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EventFlow.Demo.Infrastructure.AutofacModules
{
    public class InfrastructureModule : Module
    {
        private readonly IConfiguration Configuration;

        public InfrastructureModule(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var sqlSettings = Configuration.GetSection("Sql").Get<SqlSettings>();
            builder.RegisterInstance(Options.Create(sqlSettings));
            var options = new DbContextOptionsBuilder<DemoContext>()
               .UseSqlServer(sqlSettings.ConnectionString)
               .Options;

            builder.RegisterType<DemoContext>()
                .AsSelf()
                .InstancePerRequest()
                .InstancePerLifetimeScope()
                .WithParameter(new NamedParameter("options", options));

            builder.RegisterGeneric(typeof(ReadModelRepository<>))
                .As(typeof(IReadModelRepository<>));
        }
    }
}
