using ApplicationWithAuthorization.Model.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationWithAuthorization.Model.Database.Contexts
{
    public class UserChangeCardDbContext : DbContext
    {
        public DbSet<UserChangeCard> UserChangeCards { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=" + @"Databases\Database.db");
        }
    }
}
