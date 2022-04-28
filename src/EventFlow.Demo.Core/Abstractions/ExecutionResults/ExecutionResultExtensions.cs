using EventFlow.Demo.Core.Abstractions.ExecutionResults;
using EventFlow.Demo.Core.Exceptions;

namespace EventFlow.Aggregates.ExecutionResults
{
    public static class ExecutionResultExtensions
    {
        public static T Validate<T>(this T executionResult) where T : IExecutionResult
        {
            switch (executionResult)
            {
                case FailedExecutionResult failedResult:
                    throw new DomainException(failedResult.Errors.ToArray());
                case NotFoundExecutionResult notFoundResult:
                   throw new NotFoundException(notFoundResult.Message);
                default:
                    break;
            }

            return executionResult;
        }
    }
}
