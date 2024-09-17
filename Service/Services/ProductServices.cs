using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System.Linq.Expressions;

namespace Service.Services
{
    public class ProductServices : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductServices()
        {
            _productRepository=new ProductRepository();
        }

        public async Task CreateAsync(Product entity)
        {
            await _productRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> FilterByCategoryNameAsync(string categoryName)
        {
             return await _productRepository.FilterByCategoryNameAsync(m=>m.Name == categoryName);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
           return await _productRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetAllWithCategoryIdAsync()
        {
            return await _productRepository.GetAllWithCategoryIdAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> SearchByColorAsync(string searchText)
        {
           return await _productRepository.SearchByColorAsync(c=>c.Color.Contains(searchText));
        }

        public async Task<IEnumerable<Product>> SearchByNameAsync(string searchText)
        {
            return await _productRepository.SearchByNameAsync(c=>c.Name.Contains(searchText));
        }

        public Task<IEnumerable<Product>> SortByCreatedDateAsync()
        {
            return _productRepository.SortByCreatedDateAsync();
        }

        public Task<IEnumerable<Product>> SortWithPriceAsync()
        {
            return _productRepository.SortWithPriceAsync();
        }

        public async Task UpdateAsync(int id, Product product)
        {
            await _productRepository.UpdateAsync(id, product);
        }
    }
}
