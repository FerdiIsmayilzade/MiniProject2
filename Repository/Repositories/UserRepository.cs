using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<User> _dbSet;

        public UserRepository()
        {
            _context = new AppDbContext();
            _dbSet=_context.Set<User>();
        }
        public async Task<bool> CheckAsync(string username, string password)
        {
            return await _context.Set<User>().AnyAsync(x=>x.UserName==username  && x.Password==password);
        }
    }
}
