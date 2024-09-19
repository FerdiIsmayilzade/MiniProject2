using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryServices()
        {
            _categoryRepository=new CategoryRepository();
        }
        public async Task CreateAsync(Category entity)
        {
            await _categoryRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var existCategory= await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.DeleteAsync(existCategory);
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
           return _categoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Category>> SortWithCreatedDateAsync(int input)
        {
           return await _categoryRepository.SortWithCreatedDateAsync(input);
        }

        public async Task<IEnumerable<Category>> GetAllWithProductsAsync()
        {
            return await _categoryRepository.GetAllWithProductsAsync();
        }

        public async Task<IEnumerable<ArchiveCategories>> GetArchiveCategoriesAsync()
        {
            return await _categoryRepository.GetArchiveCategoriesAsync();   
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Category>> SearchAsync(string searchText)
        {
            return await _categoryRepository.SearchAsync(m=>m.Name.Contains(searchText));
        }

        public async Task UpdateAsync(int id, Category category)
        {
            var existCategory=await _categoryRepository.GetByIdAsync(id);
           
            if(string.IsNullOrEmpty(category.Name))
            {
                category.Name = existCategory.Name;
                await _categoryRepository.UpdateAsync(existCategory);
            }
            else
            {
                existCategory.Name = category.Name;
                await _categoryRepository.UpdateAsync(existCategory);
            }
            
        }

        
    }
}
