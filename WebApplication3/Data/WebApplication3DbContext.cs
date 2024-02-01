using Microsoft.EntityFrameworkCore;

using WebApplication3.Model;

namespace WebApplication3.Data
{
    public class WebApplication3DbContext : DbContext
    {
        public WebApplication3DbContext( DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
