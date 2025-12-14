using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.API.Services.Abstractions;
using Store.API.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services , IConfiguration _configuration)
        {
            services.AddAutoMapper(typeof(AssemplyReference).Assembly);
            services.AddScoped<IServiceManger, ServiceManger>();
            services.Configure<JwtOptions>(_configuration.GetSection("JwtOption"));
            return services;
        }
    }
}
