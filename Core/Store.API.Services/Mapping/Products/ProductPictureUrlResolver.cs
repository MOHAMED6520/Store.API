using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.API.Domain.Entities.Products;
using Store.API.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services.Mapping
{
    public class ProductPictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductResponce, string>
    {
        public string Resolve(Product source, ProductResponce destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
              return  destination.PictureUrl = $"{_configuration["BaseUrl"]}/{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
