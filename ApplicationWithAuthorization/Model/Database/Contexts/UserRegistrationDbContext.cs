using ApplicationWithAuthorization.Model.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationWithAuthorization.Model.Database.Contexts
{
    public class UserRegistrationDbContext : DbContext
    {
        public DbSet<UserRegistration> UserRegistrations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlite("Data Source=" + @"Databases\Database.db");
        }
    }
}
