

using Capstone.web.api;
using Capstone.web.api.Endpoints;
using Microsoft.EntityFrameworkCore;

namespace MyApiProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

