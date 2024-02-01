// ProductService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication3.Model;
using WebApplication3.Repositories;

namespace WebApplication3.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _productRepository.GetProductByIdAsync(productId);
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            return await _productRepository.AddProductAsync(product);
        }

        public async Task<Product> UpdateProductAsync(int productId, Product updatedProduct)
        {
            return await _productRepository.UpdateProductAsync(productId, updatedProduct);
        }

        public async Task DeleteProductAsync(int productId)
        {
            await _productRepository.DeleteProductAsync(productId);
        }

        public async Task<List<Product>> GetFilteredAndSortedProductsAsync(string filterBy, string sortBy, bool isAscending)
        {
            return await _productRepository.GetFilteredAndSortedProductsAsync(filterBy, sortBy, isAscending);
        }
    }
}
