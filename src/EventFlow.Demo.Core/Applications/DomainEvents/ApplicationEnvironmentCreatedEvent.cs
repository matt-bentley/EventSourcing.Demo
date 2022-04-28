using EventFlow.Aggregates;
using EventFlow.Demo.Core.Applications.Entities;
using EventFlow.EventStores;

namespace EventFlow.Demo.Core.Applications.DomainEvents
{
    [EventVersion("ApplicationEnvironmentCreated", 1)]
    public class ApplicationEnvironmentCreatedEvent : AggregateEvent<ApplicationEnvironment, Id>
    {
        public ApplicationEnvironmentCreatedEvent(string applicationName,
            string environmentName)
        {
            ApplicationName = applicationName;
            EnvironmentName = environmentName;
        }

        public readonly string ApplicationName;
        public readonly string EnvironmentName;
    }
}
