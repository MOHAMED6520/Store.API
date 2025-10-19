using AutoMapper;
using Store.API.Domain.Contracts;
using Store.API.Services.Abstractions;
using Store.API.Services.Abstractions.Products;
using Store.API.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services
{
    public class ServiceManger(IMapper _mapper , IUnitOfWork _unitOfWork) : IServiceManger
    {
        public IProductService productService { get; } = new ProductService(_unitOfWork, _mapper);
    }
}
