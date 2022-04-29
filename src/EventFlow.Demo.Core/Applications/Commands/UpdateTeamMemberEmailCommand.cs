using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Demo.Core.Applications.Entities;

namespace EventFlow.Demo.Core.Applications.Commands
{
    public class UpdateTeamMemberEmailCommand : Command<ApplicationEnvironment, Id, IExecutionResult>
    {
        public UpdateTeamMemberEmailCommand(Guid applicationId, Guid userId, string email) : base(applicationId)
        {
            UserId = userId;
            Email = email;
        }

        public readonly Guid UserId;
        public readonly string Email;
    }

    public class UpdateTeamMemberEmailCommandHandler : CommandHandler<ApplicationEnvironment, Id, IExecutionResult, UpdateTeamMemberEmailCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(ApplicationEnvironment aggregate,
            UpdateTeamMemberEmailCommand command,
            CancellationToken cancellationToken)
        {
            var executionResult = aggregate.UpdateTeamMemberEmail(command.UserId, command.Email);
            return Task.FromResult(executionResult);
        }
    }
}
