namespace SmartCity.Data.Entities.Bus
{
    public class BusWorker : BaseModel
    {
        public string License { get; set; }
        public virtual Bus Bus { get; set; }

    }
}
