using Microsoft.EntityFrameworkCore;
using Store.API.Domain.Contracts;
using Store.API.Domain.Entities.Products;
using Store.API.Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.API.Persistence
{
    public class DbInitializer(StoreDbContext _context) : IDbInitializer
    {

        public async Task IntializeAsync()
        {

            //Create Database
            //Update Database

            if (_context.Database.GetPendingMigrationsAsync().Result.Any())
            {
                await _context.Database.MigrateAsync();
            }

            //..\Infrastructure\Store.API.Persistence\Data\Dataseding\products.json

            

            if (!await _context.ProductBrands.AnyAsync())
            {
                //Read All Data From Json File
                var Brandsdata = await File.ReadAllTextAsync(@"..\Infrastructure\Store.API.Persistence\Data\Dataseding\brands.json");

                // converte the json string to list<ProductBrand>
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(Brandsdata);

                //Cheak if file exist and have data
                //Add All Data To productbrands table
                if (Brands is not null && Brands.Count > 0)
                {
                    await _context.AddRangeAsync(Brands);
                }
            }

            if (!await _context.ProductTypes.AnyAsync())
            {
                //Read All Data From Json File
                var TypesData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.API.Persistence\Data\Dataseding\types.json");

                // converte the json string to list<ProductTypes>
                var Types =   JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                //Cheak if file exist and have data
                //Add All Data To productTypes table
                if (Types is not null && Types.Count>0)
                {
                   await _context.AddRangeAsync(Types);
                }

            }

            if (!await _context.Products.AnyAsync())
            {
                //Read All Data From Json File
                var ProductsData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.API.Persistence\Data\Dataseding\products.json");

                // converte the json string to list<Product>
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                //Cheak if file exist and have data
                //Add All Data To product table
                if (Products is not null && Products.Count > 0)
                {
                    await _context.AddRangeAsync(Products);
                }

            }
           await _context.SaveChangesAsync();
        }
    }
}
