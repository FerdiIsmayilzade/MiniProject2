using Domain.Entities;

namespace Service.Services.Interfaces
{
    public interface ICategoryServices
    {
        Task CreateAsync(Category entity);

        Task DeleteAsync(int id);

        Task<IEnumerable<Category>> GetAllAsync();

        Task<IEnumerable<Category>> GetAllWithProductsAsync();
        Task<IEnumerable<Category>> SortWithCreatedDateAsync(int input);

        Task<IEnumerable<ArchiveCategories>> GetArchiveCategoriesAsync();

        Task<IEnumerable<Category>> SearchAsync(string searchText);

        Task UpdateAsync(int id,Category category);

        Task<Category> GetByIdAsync(int id);

    }
}
