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
        IAmReadModelFor<ApplicationEnvironment, Id, ComponentRemovedEvent>,
        IAmReadModelFor<ApplicationEnvironment, Id, TeamMemberOnboardedEvent>,
        IAmReadModelFor<ApplicationEnvironment, Id, TeamMemberOffboardedEvent>
    {
        public string Name { get; set; }
        public string EnvironmentName { get; set; }
        public List<ComponentReadModel> Components { get; set; } = new List<ComponentReadModel>();
        public List<TeamMemberReadModel> Team { get; set; } = new List<TeamMemberReadModel>();

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

        public void Apply(IReadModelContext context, IDomainEvent<ApplicationEnvironment, Id, TeamMemberOnboardedEvent> domainEvent)
        {
            Team.Add(new TeamMemberReadModel()
            {
                Email = domainEvent.AggregateEvent.Email,
                UserId = domainEvent.AggregateEvent.UserId,
            });
        }

        public void Apply(IReadModelContext context, IDomainEvent<ApplicationEnvironment, Id, TeamMemberOffboardedEvent> domainEvent)
        {
            Team.RemoveAll(e => e.UserId == domainEvent.AggregateEvent.UserId);
        }
    }
}
