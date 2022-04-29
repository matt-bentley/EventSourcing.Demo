using EventFlow.Aggregates;
using EventFlow.Demo.Core.Applications.Entities;
using EventFlow.EventStores;

namespace EventFlow.Demo.Core.Applications.DomainEvents
{
    [EventVersion("TeamMemberOffboarded", 1)]
    public class TeamMemberOffboardedEvent : AggregateEvent<ApplicationEnvironment, Id>
    {
        public TeamMemberOffboardedEvent(Guid userId)
        {
            UserId = userId;
        }

        public readonly Guid UserId;
    }
}
