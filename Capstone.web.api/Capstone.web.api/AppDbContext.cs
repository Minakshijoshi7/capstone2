

using Capstone.web.api;
using Capstone.web.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MyApiProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Category { get; set; }
        public object Categories { get; internal set; }
    }
}

