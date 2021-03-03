using SmartCity.Data.Entities.Medicine;
using SmartCity.Data.Interfaces;

namespace SmartCity.Data.Repositories
{
    public class MedicalInsuranceRepository : BaseRepository<MedicalInsurance>, IMedicalInsuranceRepository
    {
        public MedicalInsuranceRepository(SmartCityDbContext context) : base(context) { }


    }
}
