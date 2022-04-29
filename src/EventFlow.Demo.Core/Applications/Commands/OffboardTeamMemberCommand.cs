using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Demo.Core.Applications.Entities;

namespace EventFlow.Demo.Core.Applications.Commands
{
    public class OffboardTeamMemberCommand : Command<ApplicationEnvironment, Id, IExecutionResult>
    {
        public OffboardTeamMemberCommand(Guid applicationId, Guid userId) : base(applicationId)
        {
            UserId = userId;
        }

        public readonly Guid UserId;
    }

    public class OffboardTeamMemberCommandHandler : CommandHandler<ApplicationEnvironment, Id, IExecutionResult, OffboardTeamMemberCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(ApplicationEnvironment aggregate,
            OffboardTeamMemberCommand command,
            CancellationToken cancellationToken)
        {
            var executionResult = aggregate.OffboardTeamMember(command.UserId);
            return Task.FromResult(executionResult);
        }
    }
}
