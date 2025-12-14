
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Store.API.Domain.Contracts;
using Store.API.Persistence;
using Store.API.Persistence.Data.Contexts;
using Store.API.Persistence.Repositories;
using Store.API.Services;
using Store.API.Services.Abstractions;
using Store.API.Services.Mapping;
using Store.API.Shared.ErrorsModels;
using Store.API.Web.Extensions;
using Store.API.Web.Middlewares;
using System.Threading.Tasks;

namespace Store.API.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.RegisterAllServices(builder.Configuration);


            var app = builder.Build();

            await app.ConfigureMiddleWares();

            app.Run();
        }
    }
}
