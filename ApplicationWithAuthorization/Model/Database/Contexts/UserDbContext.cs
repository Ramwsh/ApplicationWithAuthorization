using Microsoft.EntityFrameworkCore;
using ApplicationWithAuthorization.Model.Database.Entities;

namespace ApplicationWithAuthorization.Model.Database.Contexts
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=" + @"Databases\Database.db");
        }
    }
}
