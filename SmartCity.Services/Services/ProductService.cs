using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartCity.Data.Entities.UserAccount;
using SmartCity.Data.Interfaces;
using SmartCity.Services.Interfaces;
using SmartCity.Services.Models;

namespace SmartCity.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize)
        {
            var productsAsQueryable = productRepository.GetAllAsQueryable();

            return await productsAsQueryable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Product> GetProductAsync(long productId)
        {
            return await productRepository.GetByIdAsync(productId);
        }
        
        public async Task CreateProductAsync(Product product)
        {
            
            await productRepository.SaveAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await productRepository.SaveAsync(product);
        }

        public async Task<bool> ProductExistsAsync(long productId)
        {
            return await productRepository.Exists(productId);
        }

        public async Task DeleteProductAsync(long productId)
        {
            await productRepository.DeleteAsync(productId);
        }
    }
}