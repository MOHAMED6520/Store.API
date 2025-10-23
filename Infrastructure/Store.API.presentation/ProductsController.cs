using Microsoft.AspNetCore.Mvc;
using Store.API.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.presentation
{
    [ApiController]
    [Route("aps/[Controller]")]
    public class ProductsController(IServiceManger _serviceManger) :ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts (int? brandId , int? TypeId , string? Sort,string? Search)
        {
            var products =await _serviceManger.productService.GetAllProductAsync(brandId, TypeId , Sort, Search);
            if (products is null) return BadRequest();

            return Ok(products);

        }

        [HttpGet]
        [Route("{id?}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if (id is null) return BadRequest();
            var products =await _serviceManger.productService.GetProductByIdAsync(id.Value);
            if (products is null) return NotFound();

            return Ok(products);

        }


        [HttpGet]
        [Route("Brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var Brands =await _serviceManger.productService.GetAllBrandsAsync();
            if (Brands is null) return NotFound();
            return Ok(Brands);
        }



        [HttpGet]
        [Route("Types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var Types =await _serviceManger.productService.GetAllTypesAsync();
            if (Types is null) return NotFound();
            return Ok(Types);
        }
    }
}
