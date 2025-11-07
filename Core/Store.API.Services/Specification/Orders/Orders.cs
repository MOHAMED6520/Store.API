using Store.API.Domain.Models.Orders;
using Store.API.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services.Specification.Orders
{
    public class OrderSpecification : BaseSpecification<Guid, Order>
    {
        public OrderSpecification(Guid id, string Email) : base(O => O.UserEmail == Email && O.Id == id)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.ShippingAddress);
            Includes.Add(O => O.Items);
        }
        public OrderSpecification( string Email) : base(O => O.UserEmail == Email)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.ShippingAddress);
            Includes.Add(O => O.Items);

        }
    }
    
}
