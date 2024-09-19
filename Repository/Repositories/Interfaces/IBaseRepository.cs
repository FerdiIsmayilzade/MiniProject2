using Domain.Common;
using Domain.Entities;
using System.Linq.Expressions;

namespace Repository.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T:BaseEntity
    {
        
        Task CreateAsync(T entity);
        
        Task DeleteAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();
        
        Task UpdateAsync(T entity);

        Task<T> GetByIdAsync(int id);







    }
}
