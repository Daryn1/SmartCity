namespace SmartCity.Web.Models.Student
{
    public class CountryViewModel
    {
        public string CountryName { get; set; }
        public string CapitalName { get; set; }
        public int Population { get; set; }
        public string Flag { get; set; }
        public double Area { get; set; }

        public CountryViewModel(string country, string capital, int population, string flag, double area)
        {
            CountryName = country;
            CapitalName = capital;
            Population = population;
            Flag = flag;
            Area = area;
        }
    }
}
