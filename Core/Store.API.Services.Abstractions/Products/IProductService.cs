using Store.API.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services.Abstractions.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponce>> GetAllProductAsync(int? brand,int? Type , string? Sort,string? Search);
        Task<ProductResponce> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync();
    }
}
