using Store.API.Domain.Entities.Products;
using Store.API.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services.Specification.Products
{
    public class ProductCountSpecification :BaseSpecification<int,Product>
    {
        public ProductCountSpecification(ProductQueryParameters parameters):base(p=>
        (
                 (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId)
                  &&
                 (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId)
                 )
                 &&
                 (
                  (string.IsNullOrEmpty(parameters.Search) || p.Name.ToLower().Contains(parameters.Search.ToLower()))
                 )
                )
        {
            
        }
    }
}
