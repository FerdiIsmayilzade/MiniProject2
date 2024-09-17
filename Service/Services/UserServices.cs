using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class UserServices:IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices()
        {
            _userRepository=new UserRepository();
        }

        public async Task CheckAsync(User user)
        {
           await _userRepository.CheckAsync(user);
        }

        public async Task CreateAsync(User entity)
        {
            await _userRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }
    }
}
