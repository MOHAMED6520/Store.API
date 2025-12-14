using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Store.API.Domain.Contracts;
using Store.API.Domain.Models.Identity;
using Store.API.Persistence.Data.Contexts;
using Store.API.Persistence.Identity;
using Store.API.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Persistence
{
    public static class AddInfrastructure
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration _configuration )
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("IdentityConnection"));
            });
            services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
            {
                return ConnectionMultiplexer.Connect(_configuration.GetConnectionString("Redis")!);
            });
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            return services;
        }
    }
}
