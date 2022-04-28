using EventFlow.Aggregates;
using EventFlow.Demo.Core.Abstractions.ReadModels;
using EventFlow.Demo.Core.Applications.DomainEvents;
using EventFlow.Demo.Core.Applications.Entities;
using EventFlow.ReadStores;

namespace EventFlow.Demo.Core.Applications.ReadModels
{
    public class ApplicationReadModel : ReadModel, 
        IAmReadModelFor<ApplicationEnvironment, Id, ApplicationEnvironmentCreatedEvent>,
        IAmReadModelFor<ApplicationEnvironment, Id, ComponentRegisteredEvent>,
        IAmReadModelFor<ApplicationEnvironment, Id, ComponentRemovedEvent>
    {
        public string Name { get; set; }
        public string EnvironmentName { get; set; }
        public List<ComponentReadModel> Components { get; set; } = new List<ComponentReadModel>();

        public void Apply(IReadModelContext context, IDomainEvent<ApplicationEnvironment, Id, ApplicationEnvironmentCreatedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Name = domainEvent.AggregateEvent.ApplicationName;
            EnvironmentName = domainEvent.AggregateEvent.EnvironmentName;
        }

        public void Apply(IReadModelContext context, IDomainEvent<ApplicationEnvironment, Id, ComponentRegisteredEvent> domainEvent)
        {
            Components.Add(new ComponentReadModel()
            {
                Id = domainEvent.AggregateEvent.ComponentId.Value,
                Name = domainEvent.AggregateEvent.Name,
                Url = domainEvent.AggregateEvent.Url,
            });
        }

        public void Apply(IReadModelContext context, IDomainEvent<ApplicationEnvironment, Id, ComponentRemovedEvent> domainEvent)
        {
            Components.RemoveAll(e => e.Id == domainEvent.AggregateEvent.ComponentId.Value);
        }
    }
}
