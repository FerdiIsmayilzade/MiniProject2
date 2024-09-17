using Domain.Entities;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class UserRepository:BaseRepository<User>,IUserRepository
    {
    }
}
