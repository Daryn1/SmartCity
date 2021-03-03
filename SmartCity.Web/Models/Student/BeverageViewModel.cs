using System.Collections.Generic;
using System.Globalization;

namespace SmartCity.Web.Models.Student
{
    public class BeverageViewModel
    {
        public string BeverageName { get; set; }
        public List<BeverageIngredient> BeverageIngredients { get; set; }

        public decimal BeveragePricePerPiece { get; set; }

        public string BeveragePriceAsString
        {
            get => BeveragePricePerPiece.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
        }

    }
    
}