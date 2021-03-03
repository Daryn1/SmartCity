using System.Collections.Generic;
using System.Linq;
using SmartCity.Data.Entities;
using SmartCity.Data.Interfaces;

namespace SmartCity.Data.Repositories
{
    public class AdressRepository : BaseRepository<Adress>, IAdressRepository
    {
        public AdressRepository(SmartCityDbContext context) : base(context)
        {
        }

        public IEnumerable<Adress> GetAdressByCity(string city)
        {
            return dbSet.Where(x => x.City == city);
        }
    }
}
