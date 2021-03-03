using System.Collections.Generic;

namespace SmartCity.Data.Entities.UserAccount
{
    public class Role : BaseModel
    {
        public virtual string Name { get; set; }

        public virtual List<CitizenUser> Users { get; set; }
    }
}
