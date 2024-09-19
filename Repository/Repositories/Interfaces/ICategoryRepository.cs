using Domain.Entities;
using System.Linq.Expressions;

namespace Repository.Repositories.Interfaces
{
    public interface ICategoryRepository:IBaseRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllWithProductsAsync();
        Task<IEnumerable<Category>> SortWithCreatedDateAsync(int input);

        Task<IEnumerable<ArchiveCategories>> GetArchiveCategoriesAsync();

        Task<IEnumerable<Category>> SearchAsync(Expression<Func<Category,bool>> expression);


    }
}
