using Onion.Core.Entities;
using Onion.Repository.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;

namespace Ecommerce.Reposatory.Context
{
    public static class OnionContextSeed
    {

        public static async Task SeedAsync(OnionContext dbContext)
        {
            // Seed Brands
            var BrandData = File.ReadAllText("../Onion.Repository/Data/DataSeed/brands.json");

            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
            if (brands?.Count > 0)
            {
                foreach (var brand in brands)
                {
                    await dbContext.Set<ProductBrand>().AddAsync(brand);
                }
            }
            await dbContext.SaveChangesAsync();

            // Seed Types
            var TypeData = File.ReadAllText("../Onion.Repository/Data/DataSeed/types.json");

            var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
            if (Types?.Count > 0)
            {
                foreach (var Type in Types)
                {
                    await dbContext.Set<ProductType>().AddAsync(Type);

                }
            }
            await dbContext.SaveChangesAsync();

            // Seed Products
            var ProductData = File.ReadAllText("../Onion.Repository/Data/DataSeed/products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(ProductData);
            if (products?.Count > 0)
            {
                foreach (var product in products)
                {
                    await dbContext.Set<Product>().AddAsync(product);

                }
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
