using System.Collections.Generic;
using SmartCity.Data.Entities.Medicine;

namespace SmartCity.Data.Interfaces
{
    public interface IMedicineCertificateRepository
    {
        MedicineCertificate Get(long id);
        List<MedicineCertificate> GetAll();
        void Save(MedicineCertificate model);
        void Delete(long id);
    }
}