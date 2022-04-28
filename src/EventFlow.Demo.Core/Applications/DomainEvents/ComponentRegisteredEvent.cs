using EventFlow.Aggregates;
using EventFlow.Demo.Core.Applications.Entities;
using EventFlow.EventStores;

namespace EventFlow.Demo.Core.Applications.DomainEvents
{
    [EventVersion("ComponentRegistered", 1)]
    public class ComponentRegisteredEvent : AggregateEvent<ApplicationEnvironment, Id>
    {
        public ComponentRegisteredEvent(Id componentId, string name, string url)
        {
            ComponentId = componentId;
            Name = name;
            Url = url;
        }

        public readonly Id ComponentId;
        public readonly string Name;
        public readonly string Url;
    }
}
