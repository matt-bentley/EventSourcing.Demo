using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;

namespace EventFlow.Demo.Application.Behaviours
{
    public class ValidatingCommandBus : ICommandBus
    {
        private readonly ICommandBus _commandBus;

        public ValidatingCommandBus(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public async Task<TExecutionResult> PublishAsync<TAggregate, TIdentity, TExecutionResult>(ICommand<TAggregate, TIdentity, TExecutionResult> command, CancellationToken cancellationToken)
            where TAggregate : IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
            where TExecutionResult : IExecutionResult
        {
            var executionResult = await _commandBus.PublishAsync(command, cancellationToken);
            return executionResult.Validate();
        }
    }
}
