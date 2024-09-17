using Domain.Entities;

namespace Service.Services.Interfaces
{
    public interface IUserServices
    {
        Task CreateAsync(User entity);

        Task DeleteAsync(int id);

        Task<IEnumerable<User>> GetAllAsync();

        Task CheckAsync(User user);

    }
}
