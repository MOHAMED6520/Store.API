using AutoMapper;
using Store.API.Domain.Contracts;
using Store.API.Domain.Entities.Products;
using Store.API.Services.Abstractions.Products;
using Store.API.Services.Specification;
using Store.API.Services.Specification.Products;
using Store.API.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services.Products
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<ProductResponce>> GetAllProductAsync(int? brand, int? Type , string? Sort , string? Search)
        {
            //var products= await _unitOfWork.GetRepository<int, Product>().GetAllAsync();
            var spec = new ProductWithBrandAndTypeSpecification(brand, Type,Sort,Search);
           var products= await _unitOfWork.GetRepository<int, Product>().GetAllAsync(spec);
            var result = _mapper.Map<IEnumerable<ProductResponce>>(products);
            return result;
        }
        public async Task<ProductResponce> GetProductByIdAsync(int id)
        {
            // var product =await _unitOfWork.GetRepository<int, Product>().GetAsync(id);

            var spec = new ProductWithBrandAndTypeSpecification(id);
            var product =await _unitOfWork.GetRepository<int, Product>().GetAsync(spec);
            var result = _mapper.Map<ProductResponce>(product);
            return result;
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync()
        {
            var Brands =await _unitOfWork.GetRepository<int, ProductBrand>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(Brands);
            return result;
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync()
        {
            var Types =await _unitOfWork.GetRepository<int, ProductType>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(Types);
            return result;
        }
     
    }
}
