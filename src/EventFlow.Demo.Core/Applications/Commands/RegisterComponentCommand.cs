using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Demo.Core.Applications.Entities;

namespace EventFlow.Demo.Core.Applications.Commands
{
    public class RegisterComponentCommand : Command<ApplicationEnvironment, Id, IExecutionResult>
    {
        public RegisterComponentCommand(Guid applicationId, Guid componentId, string name, string url) : base(applicationId)
        {
            ComponentId = componentId;
            Name = name;
            Url = url;
        }

        public readonly Guid ComponentId;
        public readonly string Name;
        public readonly string Url;
    }

    public class RegisterComponentCommandHandler : CommandHandler<ApplicationEnvironment, Id, IExecutionResult, RegisterComponentCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(ApplicationEnvironment aggregate,
            RegisterComponentCommand command,
            CancellationToken cancellationToken)
        {
            var executionResult = aggregate.RegisterComponent(command.ComponentId, command.Name, command.Url);
            return Task.FromResult(executionResult);
        }
    }
}
