using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task CheckAsync(User user);
    }
}
