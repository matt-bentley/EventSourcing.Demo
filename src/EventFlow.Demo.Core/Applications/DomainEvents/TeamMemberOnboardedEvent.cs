using EventFlow.Aggregates;
using EventFlow.Demo.Core.Applications.Entities;
using EventFlow.EventStores;

namespace EventFlow.Demo.Core.Applications.DomainEvents
{
    [EventVersion("TeamMemberOnboarded", 1)]
    public class TeamMemberOnboardedEvent : AggregateEvent<ApplicationEnvironment, Id>
    {
        public TeamMemberOnboardedEvent(Guid userId, string email)
        {
            UserId = userId;
            Email = email;
        }

        public readonly Guid UserId;
        public readonly string Email;
    }
}
