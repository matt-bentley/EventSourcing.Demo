using EventFlow.Demo.Application.Users.Commands;
using EventFlow.Demo.Application.Users.Models;
using EventFlow.Demo.Application.Users.Queries;
using EventFlow.Demo.Core.Exceptions;
using EventFlow.Demo.Core.Users.ReadModels;
using EventFlow.Queries;

namespace EventFlow.Demo.Application.Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public UsersService(ICommandBus commandBus,
            IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        public async Task<Guid> CreateAsync(UserCreateDto user)
        {
            await ValidateUniqueEmailAsync(user.Email);
            var id = Guid.NewGuid();
            await _commandBus.PublishAsync(new CreateUserCommand(id, user.FirstName, user.LastName, user.Email), CancellationToken.None);
            return id;
        }

        public async Task<UserReadModel> GetByEmailAsync(string email)
        {
            return await _queryProcessor.ProcessAsync(new GetByEmailQuery(email), CancellationToken.None);
        }

        public async Task UpdateEmailAsync(Guid id, string email)
        {
            await ValidateUniqueEmailAsync(email);
            await _commandBus.PublishAsync(new UpdateEmailCommand(id, email), CancellationToken.None);
        }

        private async Task ValidateUniqueEmailAsync(string email)
        {
            var existingUser = await GetByEmailAsync(email);
            if (existingUser != null)
            {
                throw new DomainException($"User already exists for: {email}");
            }
        }
    }

    public interface IUsersService
    {
        Task<UserReadModel> GetByEmailAsync(string email);
        Task<Guid> CreateAsync(UserCreateDto user);
        Task UpdateEmailAsync(Guid id, string email);
    }
}
