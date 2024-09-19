using Domain.Entities;
using System.Linq.Expressions;

namespace Service.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateAsync(Product entity);

        Task DeleteAsync(int id);

        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> SearchByNameAsync(string searchText);

        Task<IEnumerable<Product>> FilterByCategoryNameAsync(string categoryName);

        Task<IEnumerable<Product>> GetAllWithCategoryIdAsync();

        Task<IEnumerable<Product>> SortWithPriceAsync(int input);

        Task<IEnumerable<Product>> SortByCreatedDateAsync(int input);

        Task<IEnumerable<Product>> SearchByColorAsync(string searchText);

        Task UpdateAsync(int id, Product product);

        Task<Product> GetByIdAsync(int id);
    }
}
