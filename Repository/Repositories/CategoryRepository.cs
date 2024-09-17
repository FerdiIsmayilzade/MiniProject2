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

        public CategoryRepository()
        {
            _context = new AppDbContext();
            _dbSet=_context.Set<Category>();
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
            ArchiveCategoryRepository archiveCategoryRepository = new ArchiveCategoryRepository();
            var result = await archiveCategoryRepository.GetAllAsync();
            return result;

        }

        public async Task<Category> GetByIdAsync(int id)
        {
           return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<IEnumerable<Category>> SearchAsync(Expression<Func<Category,bool>> expression)
        {
           return await _dbSet.Where(expression).ToListAsync();
        }

            

        public async Task UpdateAsync(int id, Category category)
        {
            var existCategory=await _dbSet.FindAsync(id);
            existCategory.Name = category.Name;
            await _context.SaveChangesAsync();

        }
    }
}
