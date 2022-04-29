using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Demo.Application.Users.Services;
using EventFlow.Demo.Core;
using EventFlow.Demo.Core.Users.Entities;

namespace EventFlow.Demo.Application.Users.Commands
{
    public class UpdateEmailCommand : Command<User, Id, IExecutionResult>
    {
        public UpdateEmailCommand(Guid id, string email) : base(id)
        {
            Email = email;
        }

        public readonly string Email;
    }

    public class UpdateEmailCommandHandler : CommandHandler<User, Id, IExecutionResult, UpdateEmailCommand>
    {
        private readonly IUserValidationService _userValidationService;

        public UpdateEmailCommandHandler(IUserValidationService userValidationService)
        {
            _userValidationService = userValidationService;
        }

        public override async Task<IExecutionResult> ExecuteCommandAsync(User aggregate,
            UpdateEmailCommand command,
            CancellationToken cancellationToken)
        {
            if(await _userValidationService.EmailExists(command.Email))
            {
                return ExecutionResult.Failed($"User already exists with email: {command.Email}");
            }
            var executionResult = aggregate.UpdateEmail(command.Email);
            return executionResult;
        }
    }
}
