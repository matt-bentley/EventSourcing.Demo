using EventFlow.Aggregates.ExecutionResults;

namespace EventFlow.Demo.Core.Abstractions.ExecutionResults
{
    public abstract class CustomExecutionResult : ExecutionResult
    {
        public static IExecutionResult NotFound(string message)
        {
            return new NotFoundExecutionResult(message);
        }
    }
}
