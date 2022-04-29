using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Demo.Core.Users.DomainEvents;
using EventFlow.Demo.Core.Users.Entities.ValueObjects;

namespace EventFlow.Demo.Core.Users.Entities
{
    public class User : AggregateRoot<User, Id>,
        IEmit<UserJoinedEvent>,
        IEmit<EmailUpdatedEvent>
    {
        public UserDetails UserDetails { get; private set; }
        public string Email { get; private set; }

        public User(Id id) : base(id)
        {
        }

        public IExecutionResult Create(string firstName, string lastName, string email)
        {
            email = Denormalize(email);

            try
            {
                _ = new UserDetails(firstName, lastName);
            }
            catch (Exception ex)
            {
                return ExecutionResult.Failed(ex.Message);
            }

            if (string.IsNullOrEmpty(email))
                return ExecutionResult.Failed("Email is required");

            Emit(new UserJoinedEvent(firstName, lastName, email));

            return ExecutionResult.Success();
        }

        public IExecutionResult UpdateEmail(string email)
        {
            email = Denormalize(email);

            Emit(new EmailUpdatedEvent(email));

            return ExecutionResult.Success();
        }

        public void Apply(UserJoinedEvent @event)
        {
            UserDetails = new UserDetails(@event.FirstName, @event.LastName);
            Email = @event.Email;
        }

        public void Apply(EmailUpdatedEvent @event)
        {
            Email = @event.Email;
        }

        private string Denormalize(string value)
        {
            return value?.Trim().ToLower();
        }
    }
}
