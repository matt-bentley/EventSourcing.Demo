using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Demo.Core;
using EventFlow.Demo.Core.Users.Entities;

namespace EventFlow.Demo.Application.Users.Commands
{
    public class CreateUserCommand : Command<User, Id, IExecutionResult>
    {
        public CreateUserCommand(Guid id, string firstName, string lastName, string email) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public readonly string FirstName;
        public readonly string LastName;
        public readonly string Email;
    }

    public class CreateUserCommandHandler : CommandHandler<User, Id, IExecutionResult, CreateUserCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(User aggregate,
            CreateUserCommand command,
            CancellationToken cancellationToken)
        {
            var executionResult = aggregate.Create(command.FirstName, command.LastName, command.Email);
            return Task.FromResult(executionResult);
        }
    }
}
