using SmartCity.Data.Entities.Bus;
using SmartCity.Data.Interfaces;

namespace SmartCity.Data.Repositories
{
    public class BusRepository : BaseRepository<Bus>, IBusRepository
    {
        public BusRepository(SmartCityDbContext context) : base(context)
        {
        }

    }
}
