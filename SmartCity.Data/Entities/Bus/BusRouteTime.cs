namespace SmartCity.Data.Entities.Bus
{
    public class BusRouteTime : BaseModel
    {
        public string StartingPoint { get; set; }
        public string EndingPoint { get; set; }
        public int Minutes { get; set; }
    }
}
