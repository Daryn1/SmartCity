using SmartCity.Data.Entities.Bus;

namespace SmartCity.Data.Repositories
{
    public class BusStopRepository : BaseRepository<BusStop>
    {
        public BusStopRepository(SmartCityDbContext context) : base(context)
        {
        }

    }
}
