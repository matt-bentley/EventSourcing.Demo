using EventFlow.Aggregates;
using EventFlow.Demo.Core.Abstractions.ReadModels;
using EventFlow.Demo.Core.Applications.DomainEvents;
using EventFlow.Demo.Core.Applications.Entities;
using EventFlow.ReadStores;

namespace EventFlow.Demo.Core.Applications.ReadModels
{
    public class ApplicationSummaryReadModel : ReadModel, 
        IAmReadModelFor<ApplicationEnvironment, Id, ApplicationEnvironmentCreatedEvent>
    {
        public string Name { get; set; }
        public string EnvironmentName { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<ApplicationEnvironment, Id, ApplicationEnvironmentCreatedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Name = domainEvent.AggregateEvent.ApplicationName;
            EnvironmentName = domainEvent.AggregateEvent.EnvironmentName;
        }
    }
}
