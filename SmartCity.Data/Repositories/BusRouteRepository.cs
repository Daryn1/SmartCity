using SmartCity.Data.Entities.Bus;
using SmartCity.Data.Interfaces;

namespace SmartCity.Data.Repositories
{
    public class BusRouteRepository : BaseRepository<BusRoute>, IBusRouteRepository
    {
        public BusRouteRepository(SmartCityDbContext context) : base(context)
        {
        }

    }
}
