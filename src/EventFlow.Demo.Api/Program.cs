using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.Autofac.Extensions;
using EventFlow.Demo.Api.Infrastructure.Filters;
using EventFlow.Demo.Application.AutofacModules;
using EventFlow.Demo.Application.Behaviours;
using EventFlow.Demo.Core.Applications.Entities;
using EventFlow.Demo.Core.Applications.ReadModels;
using EventFlow.Demo.Core.AutofacModules;
using EventFlow.Demo.Core.Users.ReadModels;
using EventFlow.Demo.Infrastructure;
using EventFlow.Demo.Infrastructure.AutofacModules;
using EventFlow.Demo.Infrastructure.Providers;
using EventFlow.EntityFramework;
using EventFlow.EntityFramework.Extensions;
using EventFlow.Extensions;
using Microsoft.EntityFrameworkCore;
using EventFlow.Demo.Application.Applications.Subscriptions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
});
builder.Services.AddSwaggerGen();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new CoreModule());
    containerBuilder.RegisterModule(new ApplicationModule());
    containerBuilder.RegisterModule(new InfrastructureModule(builder.Configuration));

    EventFlowOptions.New
                    .UseAutofacContainerBuilder(containerBuilder)
                    .AddAspNetCore()
                    .RegisterServices(sr => sr.Decorate<ICommandBus>((r, cb) => new ValidatingCommandBus(cb)))
                    .AddDefaults(typeof(ApplicationEnvironment).Assembly)
                    .AddDefaults(typeof(ApplicationEmailUpdatedSubscription).Assembly)
                    .UseEntityFrameworkEventStore<DemoContext>()
                    .UseEntityFrameworkReadModel<UserReadModel, DemoContext>()
                    .UseEntityFrameworkReadModel<ApplicationReadModel, DemoContext>()
                    .UseEntityFrameworkReadModel<ApplicationSummaryReadModel, DemoContext>()
                    .ConfigureEntityFramework(EntityFrameworkConfiguration.New)
                    .AddDbContextProvider<DemoContext, DemoContextProvider>();
});

var app = builder.Build();

var context = app.Services.GetRequiredService<DemoContextProvider>().CreateContext();
context.Database.Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
