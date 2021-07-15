using SmartCity.Data.Entities.UserAccount;
using SmartCity.Data.Interfaces;

namespace SmartCity.Data.Repositories
{
    public class ProductRepository : AsyncBaseRepository<Product>, IProductRepository
    {
        public ProductRepository(SmartCityDbContext context) : base(context)
        {
        }
    }
}