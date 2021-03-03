using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity.Web.Models.Account
{
    public class AdressViewModel
    {
        public long Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int HouseNumber { get; set; }

        public string OwnerLogin { get; set; }
    }
}
