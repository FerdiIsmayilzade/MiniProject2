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

        public async Task<IEnumerable<Product>> GetAllWithCategoryIdAsync(int id)
        {
            return await _context.Products.Where(m => m.CategoryId == id).ToListAsync();
        }

        

        public async Task<IEnumerable<Product>> SearchByColorAsync(Expression<Func<Product, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchByNameAsync(Expression<Func<Product, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<Product>> SortByCreatedDateAsync(int input)
        {
            if(input == 1)
            {
                return await _dbSet.OrderBy(x => x.CreatedDate).ToListAsync();
            }
            else
            {
                return await _dbSet.OrderByDescending(x=>x.CreatedDate).ToListAsync();
            }
        }

        public async Task<IEnumerable<Product>> SortWithPriceAsync(int input)
        {
            if(input == 1)
            {
                return await _dbSet.OrderBy(c=>c.Price).ToListAsync();
            }
            else
            {
                return await _dbSet.OrderByDescending(c=>c.Price).ToListAsync();
            }
            
        }

       

        
    }
}
