using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Demo.Core.Users.DomainEvents;

namespace EventFlow.Demo.Core.Users.Entities
{
    public class User : AggregateRoot<User, Id>,
        IEmit<UserJoinedEvent>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        public User(Id id) : base(id)
        {
        }

        public IExecutionResult Create(string firstName, string lastName, string email)
        {
            firstName = firstName.Trim();
            lastName = lastName.Trim();
            email = email.Trim();

            if (string.IsNullOrEmpty(firstName))
                return ExecutionResult.Failed("First Name is required");

            if (string.IsNullOrEmpty(lastName))
                return ExecutionResult.Failed("Last Name is required");

            if (string.IsNullOrEmpty(email))
                return ExecutionResult.Failed("Email is required");

            Emit(new UserJoinedEvent(firstName, lastName, email));

            return ExecutionResult.Success();
        }

        public void Apply(UserJoinedEvent aggregateEvent)
        {
            FirstName = aggregateEvent.FirstName;
            LastName = aggregateEvent.LastName;
            Email = aggregateEvent.Email;
        }
    }
}
