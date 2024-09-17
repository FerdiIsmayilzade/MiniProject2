using Domain.Common;
using System.Linq.Expressions;

namespace Repository.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T:BaseEntity
    {
        Task CreateAsync(T entity);
        
        Task DeleteAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        



    }
}
