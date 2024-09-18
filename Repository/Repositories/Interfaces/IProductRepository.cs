using Domain.Entities;
using Repository.Data;
using System.Linq.Expressions;

namespace Repository.Repositories.Interfaces
{
    public interface IProductRepository:IBaseRepository<Product>
    {
       

        Task<IEnumerable<Product>> SearchByNameAsync(Expression<Func<Product, bool>> expression);

        Task<IEnumerable<Product>> FilterByCategoryNameAsync(Expression<Func<Product, bool>> expression);

        Task<IEnumerable<Product>> GetAllWithCategoryIdAsync();

        Task<IEnumerable<Product>> SortWithPriceAsync();

        Task<IEnumerable<Product>> SortByCreatedDateAsync();

        Task<IEnumerable<Product>> SearchByColorAsync(Expression<Func<Product, bool>> expression);



    }
}
