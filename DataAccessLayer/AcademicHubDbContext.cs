using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class AcademicHubDbContext : DbContext
    {
        public AcademicHubDbContext(DbContextOptions<AcademicHubDbContext> options) : base(options){ }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}