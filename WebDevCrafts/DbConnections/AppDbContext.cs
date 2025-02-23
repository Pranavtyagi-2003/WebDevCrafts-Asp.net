using Microsoft.EntityFrameworkCore;
using WebDevCrafts.Models.Entities;

namespace WebDevCrafts.DbConnections
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
