using AutoMapper;
using Store.API.Domain.Contracts;
using Store.API.Domain.Entities.Products;
using Store.API.Domain.Exceptions.BadRequestExceptions;
using Store.API.Domain.Exceptions.NotFoundExceptions;
using Store.API.Domain.Models.Orders;
using Store.API.Services.Abstractions.Orders;
using Store.API.Services.Specification.Orders;
using Store.API.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services.Orders
{
    public class OrderService(IUnitOfWork _unitOfWork , IMapper _mapper , IBasketRepository _basketRepository) : IOrderService
    {
        public async Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            var orderAddress = _mapper.Map<OrderAddress>(request.ShipToAddress);

            var deliveryMethod =await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(request.DeliveryMethodId);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);

            var basket =await _basketRepository.GetBasketAsynk(request.BasketId);
            if (basket is null) throw new BasketNotFoundException(request.BasketId);

            List<OrderItem> orderItems = new List<OrderItem>();

            foreach(var item in basket.Items)
            {
                var product =await _unitOfWork.GetRepository<int, Product>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);

               /* if (product.Price != item.Price) */item.Price = product.Price;

                var productInOrderItem = new ProductInOrderItem(item.Id, item.ProductName, item.PictureUrl);

                var orderItem = new OrderItem(productInOrderItem, item.Price, item.Quantity);

                orderItems.Add(orderItem);
            }
            var subTotal = orderItems.Sum(o => o.Price * o.Quantity);

            var Order = new Order(userEmail, orderAddress, deliveryMethod,orderItems, subTotal);



            await _unitOfWork.GetRepository<Guid, Order>().AddAsynk(Order);
            var count = await _unitOfWork.SaveChangesAsynk();
            if (count <= 0) throw new OrderCreateBadRequestException();
            return _mapper.Map<OrderResponse>(Order);
            
        }

        public async Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync()
        {
           var deliveryMethod =await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResponse>>(deliveryMethod);
        }

        public async Task<OrderResponse> GetOrderByIdForSpecificUserAsync(Guid id, string UserEmai)
        {
            var spec = new OrderSpecification(id, UserEmai);
           var Order =await _unitOfWork.GetRepository<Guid, Order>().GetAsync(spec);
            if (Order is null) throw new OrderNotFoundException(id);
            return _mapper.Map<OrderResponse>(Order);

        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string UserEmai)
        {
            var spec = new OrderSpecification(UserEmai);
            var Order = await _unitOfWork.GetRepository<Guid, Order>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<OrderResponse>>(Order);
        }
    }
}
