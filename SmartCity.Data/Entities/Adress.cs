namespace SmartCity.Data.Entities
{
    public class Adress : BaseModel
    {
        public string City { get; set; }

        public string Street { get; set; }

        public int HouseNumber { get; set; }

        public virtual CitizenUser Owner { get; set; }
    }
}
