using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Repository.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Product> _dbSet;

        public ProductRepository()
        {
            _context = new AppDbContext();
            _dbSet=_context.Set<Product>();
        }

        public async Task<IEnumerable<Product>> FilterByCategoryNameAsync(Expression<Func<Product, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllWithCategoryIdAsync()
        {
            return await _dbSet.Include(x=>x.CategoryId).ToListAsync();
        }

        

        public async Task<IEnumerable<Product>> SearchByColorAsync(Expression<Func<Product, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchByNameAsync(Expression<Func<Product, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<Product>> SortByCreatedDateAsync()
        {
            return await _dbSet.OrderBy(x=>x.CreatedDate).ToListAsync();
        }

        public async Task<IEnumerable<Product>> SortWithPriceAsync()
        {
            return await _dbSet.OrderBy(x=>x.Price).ToListAsync();  
        }

       

        
    }
}
