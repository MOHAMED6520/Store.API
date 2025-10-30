using Store.API.Services.Abstractions.Basket;
using Store.API.Services.Abstractions.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services.Abstractions
{
    public interface IServiceManger
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
    }
}
