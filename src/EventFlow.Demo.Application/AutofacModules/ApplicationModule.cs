using Autofac;

namespace EventFlow.Demo.Application.AutofacModules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(e => e.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }
    }
}
