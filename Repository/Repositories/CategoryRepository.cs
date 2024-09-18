using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Repository.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Category> _dbSet;
        private readonly ArchiveCategoryRepository _archiveCategoryRepository;

        public CategoryRepository()
        {
            _context = new AppDbContext();
            _dbSet=_context.Set<Category>();
            _archiveCategoryRepository = new ArchiveCategoryRepository();
        }
        public async Task<IEnumerable<Category>> SortWithCreatedDateAsync()
        {
            return await _dbSet.OrderBy(c=>c.CreatedDate).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllWithProductsAsync()
        {
            return await _dbSet.Include(x => x.Products).ToListAsync();
        }

        public async Task<IEnumerable<ArchiveCategories>> GetArchiveCategoriesAsync()
        {
            return await _archiveCategoryRepository.GetAllAsync();

        }

      

        public async Task<IEnumerable<Category>> SearchAsync(Expression<Func<Category,bool>> expression)
        {
           return await _dbSet.Where(expression).ToListAsync();
        }

            

      
    }
}
