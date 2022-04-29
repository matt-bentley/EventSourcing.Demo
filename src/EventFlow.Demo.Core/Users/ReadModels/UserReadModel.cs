using EventFlow.Aggregates;
using EventFlow.Demo.Core.Abstractions.ReadModels;
using EventFlow.Demo.Core.Users.DomainEvents;
using EventFlow.Demo.Core.Users.Entities;
using EventFlow.ReadStores;

namespace EventFlow.Demo.Core.Users.ReadModels
{
    public class UserReadModel : ReadModel, 
        IAmReadModelFor<User, Id, UserJoinedEvent>,
        IAmReadModelFor<User, Id, EmailUpdatedEvent>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<User, Id, UserJoinedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            FirstName = domainEvent.AggregateEvent.FirstName;
            LastName = domainEvent.AggregateEvent.LastName;
            Email = domainEvent.AggregateEvent.Email;
        }

        public void Apply(IReadModelContext context, IDomainEvent<User, Id, EmailUpdatedEvent> domainEvent)
        {
            Email = domainEvent.AggregateEvent.Email;
        }
    }
}
