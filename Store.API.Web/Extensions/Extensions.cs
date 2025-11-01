using Microsoft.AspNetCore.Mvc;
using Store.API.Domain.Contracts;
using Store.API.Persistence;
using Store.API.Services;
using Store.API.Shared.ErrorsModels;
using Store.API.Web.Middlewares;

namespace Store.API.Web.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddBuiltInServices();

            services.AddSwaggerGenServices();

            services.AddInfrastructureServices(_configuration);

            services.AddApplicationServices();

            services.ConfiguratinsServices();



            return services;
        }
        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }
        private static IServiceCollection AddSwaggerGenServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection ConfiguratinsServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {


                config.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var error = ActionContext.ModelState.Where(a => a.Value.Errors.Any())
                                  .Select(m => new ValidationError
                                  {
                                      Field = m.Key,
                                      Errors = m.Value.Errors.Select(m => m.ErrorMessage)
                                  });
                    var responce = new ValidationErrorResponce()
                    {
                        Errors = error
                    };

                    return new BadRequestObjectResult(responce);
                };
            });
            return services;
        }


        public static async Task<WebApplication> ConfigureMiddleWares(this WebApplication app)
        {
            await app.InitializeDataBaseAsync();

            app.GlobalErrorHandling();

            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            return app;
        }
        private static async Task<WebApplication> InitializeDataBaseAsync(this WebApplication app)
        {
            var Scope = app.Services.CreateScope();
            var dbInitializer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.IntializeAsync();
            return app;
        }
        private static WebApplication GlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}
