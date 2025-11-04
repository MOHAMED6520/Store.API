using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Store.API.Domain.Contracts;
using Store.API.Domain.Models.Identity;
using Store.API.Services.Abstractions;
using Store.API.Services.Abstractions.AuthServices;
using Store.API.Services.Abstractions.Basket;
using Store.API.Services.Abstractions.Products;
using Store.API.Services.AuthServices;
using Store.API.Services.Products;
using Store.API.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services
{
    public class ServiceManger(IMapper mapper 
        , IUnitOfWork unitOfWork,
        IBasketRepository basketRepository,
        ICacheRepository _cacheRepository,
        UserManager<AppUser> user,
        IOptions<JwtOptions> Options

        ) : IServiceManger
    {
        public IProductService ProductService { get; } = new ProductService(unitOfWork, mapper);

        public IBasketService BasketService { get; } = new BasketServices(basketRepository, mapper);
        public ICasheService CasheService { get; } = new CasheService(_cacheRepository);

        public IAuthService AuthService { get; } =new AuthService(user, Options);
    }
}
