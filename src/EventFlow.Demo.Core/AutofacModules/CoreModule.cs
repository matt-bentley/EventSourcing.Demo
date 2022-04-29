using Autofac;

namespace EventFlow.Demo.Core.AutofacModules
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(e => e.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .InstancePerLifetimeScope();
        }
    }
}
