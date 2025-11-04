using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Store.API.Domain.Contracts;
using Store.API.Domain.Models.Identity;
using Store.API.Persistence;
using Store.API.Persistence.Identity;
using Store.API.Services;
using Store.API.Shared;
using Store.API.Shared.ErrorsModels;
using Store.API.Web.Middlewares;
using System.Text;

namespace Store.API.Web.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddBuiltInServices();

            services.AddSwaggerGenServices();

            services.ConfigureServices();

            services.AddInfrastructureServices(_configuration);

            services.AddIdentityServices();

            services.AddApplicationServices(_configuration);

            services.ConfigureJwtServices(_configuration);

            return services;
        }
        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>();
            return services;
        }

        private static IServiceCollection ConfigureJwtServices(this IServiceCollection services , IConfiguration _configuration)
        {
            var JwtOption = _configuration.GetSection("JwtOption").Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,


                    ValidIssuer = JwtOption.Issuer,
                    ValidAudience = JwtOption.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOption.SecretKey)),
                };
            });
            return services;
        }

        private static IServiceCollection AddSwaggerGenServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection ConfigureServices(this IServiceCollection services)
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

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            return app;
        }
        private static async Task<WebApplication> InitializeDataBaseAsync(this WebApplication app)
        {
            var Scope = app.Services.CreateScope();
            var dbInitializer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.IntializeAsync();

            await dbInitializer.IntializeIdentityAsync();
            
            return app;
        }
        private static WebApplication GlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}
