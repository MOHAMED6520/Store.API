using AutoMapper;
using Store.API.Domain.Models.Basket;
using Store.API.Shared.Dtos.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services.Mapping
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
        }
    }
}
