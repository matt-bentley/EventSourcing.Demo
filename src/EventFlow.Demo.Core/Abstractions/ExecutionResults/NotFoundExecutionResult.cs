
namespace EventFlow.Demo.Core.Abstractions.ExecutionResults
{
    public class NotFoundExecutionResult : CustomExecutionResult
    {
        public readonly string Message;

        internal NotFoundExecutionResult(string message)
        {
            Message = message;
        }

        public override bool IsSuccess => false;
    }
}
