using System.Collections.Generic;
using SmartCity.Data.Entities.Bus;

namespace SmartCity.Data.Interfaces
{
    public interface IBusRepository
    {
        Bus Get(long id);
        List<Bus> GetAll();
        void Save(Bus model);
        void Delete(long id);
    }
}