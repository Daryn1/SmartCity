using System.Collections.Generic;
using SmartCity.Data.Entities.Medicine;

namespace SmartCity.Data.Interfaces
{
    public interface IMedicalInsuranceRepository
    {
        MedicalInsurance Get(long id);
        List<MedicalInsurance> GetAll();
        void Save(MedicalInsurance model);
        void Delete(long id);
    }
}