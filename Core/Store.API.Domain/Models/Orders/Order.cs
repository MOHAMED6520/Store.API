using Store.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Domain.Models.Orders
{
    public class Order :BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items , decimal subTotal)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string UserEmail { get; set; }
        public DateTimeOffset OrderTime { get; set; } = DateTimeOffset.Now;
        public OrderStatus status { get; set; } = OrderStatus.Pending;
        public OrderAddress ShippingAddress  { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public int DeliveryMethodId { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public decimal SubTotal { get; set; }

        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;
    }
}
