using System;

namespace SmartCity.Data.Entities.Medicine
{
    public class RecordForm : BaseModel
    {
        public virtual string Name { get; set; }

        public virtual string LastName { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual DateTime DateTime { get; set; }

        public virtual CitizenUser Citizen { get; set; }
    }
}
