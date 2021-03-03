using SmartCity.Data.Entities.Medicine;
using SmartCity.Data.Interfaces;

namespace SmartCity.Data.Repositories
{
    public class ReceptionOfPatientsRepository : BaseRepository<ReceptionOfPatients>, IReceptionOfPatientsRepository
    {
        public ReceptionOfPatientsRepository(SmartCityDbContext context) : base(context) { }

        
    }
}
