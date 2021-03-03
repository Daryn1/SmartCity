using System.Collections.Generic;
using SmartCity.Data.Entities.Medicine;

namespace SmartCity.Data.Interfaces
{
    public interface IRecordFormRepository
    {
        RecordForm Get(long id);
        List<RecordForm> GetAll();
        void Save(RecordForm model);
        void Delete(long id);
    }
}