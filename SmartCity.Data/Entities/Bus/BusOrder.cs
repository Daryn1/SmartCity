using System;

namespace SmartCity.Data.Entities.Bus
{
    public class BusOrder : BaseModel
    {
        public string Route { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime TargetedDate { get; set; }
        public string OrderDescription { get; set; }

    }
}
