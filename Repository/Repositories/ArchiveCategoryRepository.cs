using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class ArchiveCategoryRepository : IArchiveCategoryRepository
    {
        private readonly AppDbContext _context;
        private DbSet<ArchiveCategories> _dbSet;

        public ArchiveCategoryRepository()
        {
            _context = new AppDbContext();
            _dbSet=_context.Set<ArchiveCategories>();
        }
        public async Task<IEnumerable<ArchiveCategories>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
