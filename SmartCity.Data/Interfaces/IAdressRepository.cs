using System.Collections.Generic;
using SmartCity.Data.Entities;

namespace SmartCity.Data.Interfaces
{
    public interface IAdressRepository
    {
        IEnumerable<Adress> GetAdressByCity(string city);
        Adress Get(long id);
        List<Adress> GetAll();
        void Save(Adress model);
        void Delete(long id);
    }
}