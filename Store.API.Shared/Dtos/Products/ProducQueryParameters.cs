using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Shared.Dtos.Products
{
    public class ProductQueryParameters
    {
        // int? brandId , int? TypeId , string? Sort,string? Search,int? PageIndex =1,int?PageSize=5

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;

    }
}
