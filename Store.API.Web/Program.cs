
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Store.API.Domain.Contracts;
using Store.API.Persistence;
using Store.API.Persistence.Data.Contexts;
using Store.API.Services;
using Store.API.Services.Abstractions;
using Store.API.Services.Mapping;
using Store.API.Shared.ErrorsModels;
using Store.API.Web.Middlewares;
using System.Threading.Tasks;

namespace Store.API.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(p => p.AddProfile(new ProductProfile(builder.Configuration)));
            builder.Services.AddScoped<IServiceManger, ServiceManger>();
            builder.Services.Configure<ApiBehaviorOptions>(config => 
            {

                
                config.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                   var error = ActionContext.ModelState.Where(a => a.Value.Errors.Any())
                                 .Select(m=> new ValidationError
                                 {
                                     Field = m.Key,
                                     Errors =m.Value.Errors.Select(m=>m.ErrorMessage)
                                 });
                    var responce = new ValidationErrorResponce() 
                    {
                    Errors = error
                    };

                    return new BadRequestObjectResult(responce);
                };
            });

            var app = builder.Build();

            #region Initialize Db

            var Scope = app.Services.CreateScope();

            var dbInitializer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>();

            await dbInitializer.IntializeAsync();

            #endregion

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

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

            app.Run();
        }
    }
}
