
using global::WebApplication3.Data;
using global::WebApplication3.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.Model;

namespace WebApplication3.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebApplication3DbContext _dbContext;

        public ProductRepository(WebApplication3DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.Include(p => p.Categories).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _dbContext.Products.Include(p => p.Categories)
                                            .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(int productId, Product updatedProduct)
        {
            var existingProduct = await _dbContext.Products.FindAsync(productId);

            if (existingProduct != null)
            {
                // Update only the fields that are provided in the request body
                existingProduct.Name = updatedProduct.Name ?? existingProduct.Name;
                existingProduct.Price = updatedProduct.Price != 0 ? updatedProduct.Price : existingProduct.Price;
                existingProduct.Qty = updatedProduct.Qty != 0 ? updatedProduct.Qty : existingProduct.Qty;
                existingProduct.ImageUrl = updatedProduct.ImageUrl ?? existingProduct.ImageUrl;
                existingProduct.CategoryId = updatedProduct.CategoryId != 0 ? updatedProduct.CategoryId : existingProduct.CategoryId;

                _dbContext.Update(existingProduct);
                await _dbContext.SaveChangesAsync();
            }

            return existingProduct;
        }

        public async Task DeleteProductAsync(int productId)
        {
            var existingProduct = await _dbContext.Products.FindAsync(productId);

            if (existingProduct != null)
            {
                _dbContext.Products.Remove(existingProduct);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetFilteredAndSortedProductsAsync(string filterBy, string sortBy, bool isAscending)
        {
            IQueryable<Product> query = _dbContext.Products.Include(p => p.Categories);

            // Filter products
            if (!string.IsNullOrEmpty(filterBy))
            {
                query = query.Where(p => p.Name.Contains(filterBy) || p.Categories.Name.Contains(filterBy));
            }

            // Sort products
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        query = isAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                        break;
                    case "price":
                        query = isAscending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
                        break;
                        // Add more cases for other fields if needed
                }
            }

            return await query.ToListAsync();
        }
    }
}

