using System.Collections.Generic;
using SmartCity.Data.Entities.Medicine;

namespace SmartCity.Data.Interfaces
{
    public interface IReceptionOfPatientsRepository
    {
        ReceptionOfPatients Get(long id);
        List<ReceptionOfPatients> GetAll();
        void Save(ReceptionOfPatients model);
        void Delete(long id);
    }
}