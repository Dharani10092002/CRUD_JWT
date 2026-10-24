using CRUD_JWT.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUD_JWT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {
        }
    public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

