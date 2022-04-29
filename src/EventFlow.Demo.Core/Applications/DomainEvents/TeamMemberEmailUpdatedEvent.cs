using EventFlow.Aggregates;
using EventFlow.Demo.Core.Applications.Entities;
using EventFlow.EventStores;

namespace EventFlow.Demo.Core.Applications.DomainEvents
{
    [EventVersion("TeamMemberEmailUpdated", 1)]
    public class TeamMemberEmailUpdatedEvent : AggregateEvent<ApplicationEnvironment, Id>
    {
        public TeamMemberEmailUpdatedEvent(Id userId, string email)
        {
            UserId = userId;
            Email = email;
        }

        public readonly Id UserId;
        public readonly string Email;
    }
}
