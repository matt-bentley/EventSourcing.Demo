using EventFlow.Aggregates;
using EventFlow.Demo.Core.Applications.Entities;
using EventFlow.EventStores;

namespace EventFlow.Demo.Core.Applications.DomainEvents
{
    [EventVersion("ComponentRemoved", 1)]
    public class ComponentRemovedEvent : AggregateEvent<ApplicationEnvironment, Id>
    {
        public ComponentRemovedEvent(Id componentId)
        {
            ComponentId = componentId;
        }

        public readonly Id ComponentId;
    }
}
