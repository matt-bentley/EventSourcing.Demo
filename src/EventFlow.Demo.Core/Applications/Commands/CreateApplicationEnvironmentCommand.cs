using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Demo.Core.Applications.Entities;

namespace EventFlow.Demo.Core.Applications.Commands
{
    public class CreateApplicationEnvironmentCommand : Command<ApplicationEnvironment, Id, IExecutionResult>
    {
        public CreateApplicationEnvironmentCommand(Guid id, string applicationName, string environmentName) : base(id)
        {
            ApplicationName = applicationName;
            EnvironmentName = environmentName;
        }

        public readonly string ApplicationName;
        public readonly string EnvironmentName;
    }

    public class CreateApplicationEnvironmentCommandHandler : CommandHandler<ApplicationEnvironment, Id, IExecutionResult, CreateApplicationEnvironmentCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(ApplicationEnvironment aggregate,
            CreateApplicationEnvironmentCommand command,
            CancellationToken cancellationToken)
        {
            var executionResult = aggregate.Create(command.ApplicationName, command.EnvironmentName);
            return Task.FromResult(executionResult);
        }
    }
}
