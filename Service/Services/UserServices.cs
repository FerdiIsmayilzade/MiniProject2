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

        public async Task<bool> CheckAsync(string username, string password)
        {
            return await _userRepository.CheckAsync(username,password);
        }

        public async Task CreateAsync(User entity)
        {
            await _userRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var existUser=await _userRepository.GetByIdAsync(id);
            await _userRepository.DeleteAsync(existUser);
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            return _userRepository.GetByIdAsync(id);
        }
    }
}
