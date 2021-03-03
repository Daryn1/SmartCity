using System.Collections.Generic;

namespace SmartCity.Data.Entities.Bus
{
    public class BusRoute : BaseModel
    {
        public string Route { get; set; }

        public virtual List<Bus> Buses { get; set; }

    }
}
