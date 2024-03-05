using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options) : base(Options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ImageModel> Images { get; set; }
    }
}
