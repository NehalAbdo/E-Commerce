using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data
{
    public static class DataContextSeed
    {
        public static async Task SeedDataAsync(DataContext context)
        {
            if (!context.Set<ProductBrand>().Any())
            {
                var brandsData = await File.
               ReadAllTextAsync(@"..\E_Commerce.Repository\Data\DataSeeding\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands is not null && brands.Any())
                {
                    await context.Set<ProductBrand>().AddRangeAsync(brands);
                    await context.SaveChangesAsync();
                }
            }

           if(!context.Set<ProductType>().Any()) 
            {
                var typesData = await File.
                    ReadAllTextAsync(@"..\E_Commerce.Repository\Data\DataSeeding\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                if (types is not null && types.Any())
                {
                    await context.Set<ProductType>().AddRangeAsync(types);
                    await context.SaveChangesAsync();
                }
            }
           if (!context.Set<Product>().Any())
            {
                var productsData = await File.
                    ReadAllTextAsync(@"..\E_Commerce.Repository\Data\DataSeeding\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products is not null && products.Any())
                {
                    await context.Set<Product>().AddRangeAsync(products);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
