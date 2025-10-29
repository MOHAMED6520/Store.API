using Store.API.Shared;
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
        Task<PaginationResponse<ProductResponce>> GetAllProductAsync(ProductQueryParameters parameters);
        Task<ProductResponce> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync();
    }
}
