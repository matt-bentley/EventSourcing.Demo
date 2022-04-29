using EventFlow.Demo.Core.Abstractions.Repositories;
using EventFlow.Demo.Core.Users.ReadModels;

namespace EventFlow.Demo.Application.Users.Services
{
    public class UserValidationService : IUserValidationService
    {
        private readonly IReadModelRepository<UserReadModel> _usersRepository;

        public UserValidationService(IReadModelRepository<UserReadModel> usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<bool> EmailExists(string email)
        {
            var existingUser = await _usersRepository.FirstOrDefaultAsync(x => x.Email == email.Trim().ToLower());
            return existingUser != null;
        }
    }

    public interface IUserValidationService
    {
        Task<bool> EmailExists(string email);
    }
}
