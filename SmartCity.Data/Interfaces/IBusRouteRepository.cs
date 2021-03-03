using System.Collections.Generic;
using SmartCity.Data.Entities.Bus;

namespace SmartCity.Data.Interfaces
{
    public interface IBusRouteRepository
    {
        BusRoute Get(long id);
        List<BusRoute> GetAll();
        void Save(BusRoute model);
        void Delete(long id);
    }
}