using ProductAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);

        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(Guid productId);

        Task<Product> DeleteProductAsync(Guid productId);

        Task<Product> UpdateProductAsync(Guid productId, Product updatedProduct);
    }
}
