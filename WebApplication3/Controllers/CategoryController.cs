using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.Model;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly WebApplication3DbContext _dbContext;

        public CategoryController(WebApplication3DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);

            if (category == null)
            {
                return NotFound($"Category with ID {categoryId} not found.");
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category categoryRequest)
        {
            if (categoryRequest == null)
            {
                return BadRequest("Category data is invalid.");
            }

            await _dbContext.Categories.AddAsync(categoryRequest);
            await _dbContext.SaveChangesAsync();

            return Ok(categoryRequest);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] Category updatedCategory)
        {
            if (updatedCategory == null)
            {
                return BadRequest("Invalid request body.");
            }

            var existingCategory = await _dbContext.Categories.FindAsync(categoryId);

            if (existingCategory == null)
            {
                return NotFound($"Category with ID {categoryId} not found.");
            }

            // Update only the fields that are provided in the request body
            if (updatedCategory.Name != null)
            {
                existingCategory.Name = updatedCategory.Name;
            }

            _dbContext.Update(existingCategory);
            await _dbContext.SaveChangesAsync();

            return Ok(existingCategory);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var existingCategory = await _dbContext.Categories.FindAsync(categoryId);

            if (existingCategory == null)
            {
                return NotFound($"Category with ID {categoryId} not found.");
            }

            _dbContext.Categories.Remove(existingCategory);
            await _dbContext.SaveChangesAsync();
            return Ok(new { message = $"Category with ID {categoryId} has been deleted." });
        }
    }
}
