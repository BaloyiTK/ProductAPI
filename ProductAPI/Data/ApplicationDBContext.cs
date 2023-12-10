using Microsoft.EntityFrameworkCore;
using ProductAPI.Models.Domain;

namespace ProductAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
