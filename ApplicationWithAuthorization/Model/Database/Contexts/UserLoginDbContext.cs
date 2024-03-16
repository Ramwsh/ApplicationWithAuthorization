using ApplicationWithAuthorization.Model.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationWithAuthorization.Model.Database.Contexts
{
    public class UserLoginDbContext : DbContext
    {
        public DbSet<UserLogin> UserLogins { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source="+@"Databases\Database.db");
        }
    }
}
