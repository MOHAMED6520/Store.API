using AutoMapper;
using Store.API.Domain.Contracts;
using Store.API.Services.Abstractions;
using Store.API.Services.Abstractions.Basket;
using Store.API.Services.Abstractions.Products;
using Store.API.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services
{
    public class ServiceManger(IMapper mapper 
        , IUnitOfWork unitOfWork,
        IBasketRepository basketRepository) : IServiceManger
    {
        public IProductService ProductService { get; } = new ProductService(unitOfWork, mapper);

        public IBasketService BasketService { get; } = new BasketServices(basketRepository, mapper);
    }
}
