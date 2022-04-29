using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Demo.Core.Abstractions.Repositories;
using EventFlow.Demo.Core.Applications.Entities;
using EventFlow.Demo.Core.Exceptions;
using EventFlow.Demo.Core.Users.ReadModels;

namespace EventFlow.Demo.Core.Applications.Commands
{
    public class OnboardTeamMemberCommand : Command<ApplicationEnvironment, Id, IExecutionResult>
    {
        public OnboardTeamMemberCommand(Guid applicationId, Guid userId) : base(applicationId)
        {
            UserId = userId;
        }

        public readonly Guid UserId;
    }

    public class OnboardTeamMemberCommandHandler : CommandHandler<ApplicationEnvironment, Id, IExecutionResult, OnboardTeamMemberCommand>
    {
        private readonly IReadModelRepository<UserReadModel> _usersRepository;

        public OnboardTeamMemberCommandHandler(IReadModelRepository<UserReadModel> usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public override async Task<IExecutionResult> ExecuteCommandAsync(ApplicationEnvironment aggregate,
            OnboardTeamMemberCommand command,
            CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetByIdAsync(command.UserId.ToString());
            if(user == null)
            {
                throw new NotFoundException($"User not found with id: {command.UserId}");
            }
            var executionResult = aggregate.OnboardTeamMember(command.UserId, user.Email);
            return executionResult;
        }
    }
}
