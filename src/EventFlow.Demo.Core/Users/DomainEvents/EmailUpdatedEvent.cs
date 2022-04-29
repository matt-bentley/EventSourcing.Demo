using EventFlow.Aggregates;
using EventFlow.Demo.Core.Users.Entities;
using EventFlow.EventStores;

namespace EventFlow.Demo.Core.Users.DomainEvents
{
    [EventVersion("EmailUpdated", 1)]
    public class EmailUpdatedEvent : AggregateEvent<User, Id>
    {
        public EmailUpdatedEvent(string email)
        {
            Email = email;
        }

        public readonly string Email;
    }
}
