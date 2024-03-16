using ApplicationWithAuthorization.Model.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationWithAuthorization.Model.Database.Contexts
{
    public class MessagesDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=" + @"Databases\Database.db");
        }
    }
}
