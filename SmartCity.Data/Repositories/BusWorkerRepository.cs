using SmartCity.Data.Entities.Bus;

namespace SmartCity.Data.Repositories
{
    public class BusWorkerRepository : BaseRepository<Bus>
    {
        public BusWorkerRepository(SmartCityDbContext context) : base(context)
        {
        }

    }
}
