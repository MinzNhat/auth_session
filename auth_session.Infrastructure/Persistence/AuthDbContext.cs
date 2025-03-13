using Microsoft.EntityFrameworkCore;
using auth_session.Core.Entities.Auth;

namespace auth_session.Infrastructure.Persistence
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         var connectionString = Environment.GetEnvironmentVariable("DB_AUTHDB_CONNECTION");
        //         if (string.IsNullOrEmpty(connectionString))
        //         {
        //             throw new InvalidOperationException("Database connection string is not set.");
        //         }
        //         optionsBuilder.UseSqlServer(connectionString);
        //     }
        // }
    }
}
