using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using auth_session.API.Filters.Response;
using auth_session.API.Services.Auth;
using auth_session.Core.Interfaces.Auth;
using auth_session.Infrastructure.Persistence;
using auth_session.Infrastructure.Repositories.Auth;

namespace auth_session.API.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Load environment variables
            Env.Load();

            // Register required services
            services.AddHttpContextAccessor();

            var authConnectionString = Environment.GetEnvironmentVariable("DB_AUTHDB_CONNECTION") ?? "";
            var connectionString = Environment.GetEnvironmentVariable("DB_SALESPROJDB_CONNECTION") ?? "";
            var dwConnectionString = Environment.GetEnvironmentVariable("DB_DATAWAREHOUSE_CONNECTION") ?? "";

            // Register Database Contexts
            services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(authConnectionString));
            services.AddDbContext<SalesProjDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<DataWarehouseDbContext>(options => options.UseSqlServer(dwConnectionString));

            // Configure Controllers & Filters
            services.AddControllers(options =>
            {
                options.Filters.Add<ResponseFilter>();
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            // Suppress automatic model validation
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Register Repositories & Services
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(typeof(Program));

            // Configure Session
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(12);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Register Swagger (for API documentation)
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}