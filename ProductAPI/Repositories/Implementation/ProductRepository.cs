using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data; // Assuming ApplicationDBContext is in the ProductAPI.Data namespace
using ProductAPI.Models.Domain;
using ProductAPI.Repositories.Interface;

namespace ProductAPI.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext dbContext;

        public ProductRepository(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetProductByIdAsync(Guid Id)
        {
            return await dbContext.Products.FindAsync(Id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<Product> DeleteProductAsync (Guid productId)
        {
            var product = await dbContext.Products.FindAsync(productId);

            if (product != null)
            {
                dbContext.Products.Remove(product);
                await dbContext.SaveChangesAsync();
            }

            return product;
        }

        public async Task<Product> UpdateProductAsync(Guid productId, Product updatedProduct)
        {
            var existingProduct = await dbContext.Products.FindAsync(productId);

            if (existingProduct == null)
            {
                return null; // or throw an exception if you prefer
            }

            // Update the existing product with the new data
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return existingProduct;
        }


    }
}
