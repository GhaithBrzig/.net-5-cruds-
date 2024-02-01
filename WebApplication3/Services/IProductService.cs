using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int productId);

        Task<Product> AddProductAsync(Product product);

        Task<Product> UpdateProductAsync(int productId, Product updatedProduct);

        Task DeleteProductAsync(int productId);

        Task<List<Product>> GetFilteredAndSortedProductsAsync(string filterBy, string sortBy, bool isAscending);
    }
}
