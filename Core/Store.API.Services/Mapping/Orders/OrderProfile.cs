using AutoMapper;
using Store.API.Domain.Models.Orders;
using Store.API.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services.Mapping.Orders
{
    public class OrderProfile :Profile
    {
        public OrderProfile()
        {
            
            CreateMap<OrderAddressDto, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderResponse>()
                .ForMember(O => O.DeliveryMethod, M => M.MapFrom(O => O.DeliveryMethod.ShortName))
                .ForMember(OR => OR.Total, M => M.MapFrom(O => O.GetTotal()));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(I => I.ProductId, M => M.MapFrom(I => I.Product.ProductId))
                .ForMember(I => I.ProductName, M => M.MapFrom(I => I.Product.ProductName))
                .ForMember(I => I.PictureUrl, M => M.MapFrom(I => I.Product.PictureUrl));
            CreateMap<DeliveryMethod, DeliveryMethodResponse>();
                
        }
    }
}
