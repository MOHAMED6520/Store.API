using Microsoft.Extensions.DependencyInjection;
using Store.API.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AssemplyReference).Assembly);
            services.AddScoped<IServiceManger, ServiceManger>();
            return services;
        }
    }
}
