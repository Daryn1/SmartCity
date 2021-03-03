using SmartCity.Data.Entities.Medicine;
using SmartCity.Data.Interfaces;

namespace SmartCity.Data.Repositories
{
    public class MedicineCertificateRepository : BaseRepository<MedicineCertificate>, IMedicineCertificateRepository
    {
        public MedicineCertificateRepository(SmartCityDbContext context) : base(context) { }

    }
}
