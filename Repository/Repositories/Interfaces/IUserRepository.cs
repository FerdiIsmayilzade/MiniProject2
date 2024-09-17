using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task<bool> CheckAsync(string username,string password);
    }
}
