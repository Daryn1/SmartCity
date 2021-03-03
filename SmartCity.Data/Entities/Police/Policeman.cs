using System.Collections.Generic;
using SmartCity.Common.Enums;

namespace SmartCity.Data.Entities.Police
{
    public class Policeman : BaseModel
    {
        public virtual CitizenUser User { get; set; }
        
        public PolicemanRank Rank { get; set; }

        public virtual List<Violation> Violations { get; set; }
    }
}
