using EventFlow.Aggregates;
using EventFlow.Demo.Core.Users.Entities;
using EventFlow.EventStores;

namespace EventFlow.Demo.Core.Users.DomainEvents
{
    [EventVersion("UserJoined", 1)]
    public class UserJoinedEvent : AggregateEvent<User, Id>
    {
        public UserJoinedEvent(string firstName,
            string lastName,
            string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public readonly string FirstName;
        public readonly string LastName;
        public readonly string Email;
    }
}
