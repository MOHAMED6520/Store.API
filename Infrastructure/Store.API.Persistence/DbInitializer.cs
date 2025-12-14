using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.API.Domain.Contracts;
using Store.API.Domain.Entities.Products;
using Store.API.Domain.Models.Identity;
using Store.API.Domain.Models.Orders;
using Store.API.Persistence.Data.Contexts;
using Store.API.Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.API.Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;
        private readonly StoreIdentityDbContext _identityDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(StoreDbContext context,
               StoreIdentityDbContext identityDbContext,
               UserManager<AppUser> user,
               RoleManager<IdentityRole> role

            )
        {
            _context = context;
            _identityDbContext = identityDbContext;
            _userManager = user;
            _roleManager = role;
        }
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

        public async Task IntializeIdentityAsync()
        {
            if(_identityDbContext.Database.GetPendingMigrationsAsync().Result.Any())
            {
                await _identityDbContext.Database.MigrateAsync();
            }

            if(!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await _roleManager.CreateAsync(new IdentityRole() { Name = "SuperAdmin" });
            }
            if(!_userManager.Users.Any())
            {
                var superAdmin = new AppUser()
                {
                    DisplayName = "Super Admin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "01090511679"
                };
                var admin = new AppUser()
                {
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "01090511679"
                };
               await _userManager.CreateAsync(superAdmin, "Super@12345");
               await _userManager.CreateAsync(admin, "Super@12345");
               await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
               await _userManager.AddToRoleAsync(admin, "Admin");
            }

            if (!await _context.deliveryMethods.AnyAsync())
            {
                //Read All Data From Json File
                var deliveriesData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.API.Persistence\Data\Dataseding\delivery.json");

                // converte the json string to list<Delivery>
                var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveriesData);

                //Cheak if file exist and have data
                //Add All Data To Delivery table
                if (delivery is not null && delivery.Count > 0)
                {
                    await _context.AddRangeAsync(delivery);
                }

            }
            await _context.SaveChangesAsync();
        }
    }
}

