using SmartCity.Data.Entities.Medicine;
using SmartCity.Data.Interfaces;

namespace SmartCity.Data.Repositories
{
    public class RecordFormRepository : BaseRepository<RecordForm>, IRecordFormRepository
    {
        public RecordFormRepository(SmartCityDbContext context) : base(context) { }

        
    }
}
