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
    public class ProductProfile:Profile
    {
        public ProductProfile(IConfiguration _configuration)
        {
            CreateMap<Product, ProductResponce>().ForMember(B => B.Brand, O => O.MapFrom(N => N.Brand.Name))
                                                 .ForMember(B => B.Type, O => O.MapFrom(N => N.Type.Name))
                                                 .ForMember(B => B.PictureUrl, O => O.MapFrom(new ProductPictureUrlResolver(_configuration)));


            CreateMap<ProductBrand, BrandTypeResponse>();
            CreateMap<ProductType, BrandTypeResponse>();
        }
    }
}
