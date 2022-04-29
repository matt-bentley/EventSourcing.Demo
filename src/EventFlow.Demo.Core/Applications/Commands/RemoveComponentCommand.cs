using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Demo.Core.Applications.Entities;

namespace EventFlow.Demo.Core.Applications.Commands
{
    public class RemoveComponentCommand : Command<ApplicationEnvironment, Id, IExecutionResult>
    {
        public RemoveComponentCommand(Guid applicationId, Guid componentId) : base(applicationId)
        {
            ComponentId = componentId;
        }

        public readonly Guid ComponentId;
    }

    public class RemoveComponentCommandHandler : CommandHandler<ApplicationEnvironment, Id, IExecutionResult, RemoveComponentCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(ApplicationEnvironment aggregate,
            RemoveComponentCommand command,
            CancellationToken cancellationToken)
        {
            var executionResult = aggregate.RemoveComponent(command.ComponentId);
            return Task.FromResult(executionResult);
        }
    }
}
