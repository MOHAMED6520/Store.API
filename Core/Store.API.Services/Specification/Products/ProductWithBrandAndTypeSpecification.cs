using Store.API.Domain.Contracts;
using Store.API.Domain.Entities.Products;
using Store.API.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services.Specification.Products
{
    public class ProductWithBrandAndTypeSpecification :BaseSpecification<int,Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductQueryParameters parameters) :base
            (
            p =>
                 (
                 (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId)
                  &&   
                 (!parameters.TypeId.HasValue || p.TypeId== parameters.TypeId)
                 )
                 &&
                 (
                  (string.IsNullOrEmpty(parameters.Search)||p.Name.ToLower().Contains(parameters.Search.ToLower()))
                 )
                
                  
                 
             )
            
        {
            ApplyIncludes();

            if (!string.IsNullOrEmpty(parameters.Sort))
            {
                switch (parameters.Sort.ToLower())
                {
                    case "priceasc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }
            ApplyPagination(parameters.PageIndex, parameters.PageSize);
        }

        public ProductWithBrandAndTypeSpecification(int id) : base(p=>p.Id==id)
        {
            ApplyIncludes();
        }

        private void ApplyIncludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type);
        }

    }
}
