using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.API.presentation.Attributes;
using Store.API.Services.Abstractions;
using Store.API.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.presentation
{
    [ApiController]
    [Route("api/[Controller]")]
    
    public class ProductsController(IServiceManger _serviceManger) :ControllerBase
    {
        [HttpGet]
        [Cashe(100)]
        [Authorize]
        public async Task<IActionResult> GetAllProducts ([FromQuery]ProductQueryParameters parameters)
        {
            var products =await _serviceManger.ProductService.GetAllProductAsync(parameters);
            if (products is null) return BadRequest();

            return Ok(products);

        }

        [HttpGet]
        [Route("{id?}")]
        [Authorize]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if (id is null) return BadRequest();
            var products =await _serviceManger.ProductService.GetProductByIdAsync(id.Value);
            if (products is null) return NotFound();

            return Ok(products);

        }


        [HttpGet]
        [Route("Brands")]
        [Authorize]
        public async Task<IActionResult> GetAllBrands()
        {
            var Brands =await _serviceManger.ProductService.GetAllBrandsAsync();
            if (Brands is null) return NotFound();
            return Ok(Brands);
        }



        [HttpGet]
        [Route("Types")]
        [Authorize]
        public async Task<IActionResult> GetAllTypes()
        {
            var Types =await _serviceManger.ProductService.GetAllTypesAsync();
            if (Types is null) return NotFound();
            return Ok(Types);
        }
    }
}
