using Microsoft.EntityFrameworkCore;

namespace auth_session.Infrastructure.Persistence
{
    public class SalesProjDbContext(DbContextOptions<SalesProjDbContext> options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("DB_SALESPROJDB_CONNECTION") ?? "";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
