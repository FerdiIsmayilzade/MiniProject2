using Domain.Common;
using Domain.Entities;
using System.Linq.Expressions;

namespace Repository.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T:BaseEntity
    {
        Task CreateAsync(T entity);
        
        Task DeleteAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();
        
        Task UpdateAsync(int id, Category category);

        Task<T> GetByIdAsync(int id);







    }
}
