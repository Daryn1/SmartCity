using System.Collections.Generic;
using System.Threading.Tasks;
using SmartCity.Data.Entities.UserAccount;

namespace SmartCity.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize);
        Task<Product> GetProductAsync(long productId);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task<bool> ProductExistsAsync(long productId);
        Task DeleteProductAsync(long productId);
    }
}