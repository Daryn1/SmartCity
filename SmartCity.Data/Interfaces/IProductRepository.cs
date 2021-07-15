using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCity.Data.Entities.UserAccount;

namespace SmartCity.Data.Interfaces
{
    public interface IProductRepository
    {
        Task SaveAsync(Product product);
        Task<Product> GetByIdAsync(long id);
        Task<List<Product>> GetAllAsync();
        IQueryable<Product> GetAllAsQueryable();
        Task DeleteAsync(long id);
        Task<bool> Exists(long id);
    }
}