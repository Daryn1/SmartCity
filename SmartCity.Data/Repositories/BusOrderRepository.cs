using SmartCity.Data.Entities.Bus;

namespace SmartCity.Data.Repositories
{
    public class BusOrderRepository : BaseRepository<Bus>
    {
        public BusOrderRepository(SmartCityDbContext context) : base(context)
        {
        }

    }
}
