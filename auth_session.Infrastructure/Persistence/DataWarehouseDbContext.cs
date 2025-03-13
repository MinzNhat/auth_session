using Microsoft.EntityFrameworkCore;

namespace auth_session.Infrastructure.Persistence
{
    public class DataWarehouseDbContext(DbContextOptions<DataWarehouseDbContext> options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("DB_DATAWAREHOUSE_CONNECTION") ?? "";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
