using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartCity.Data.Interfaces;

namespace SmartCity.Web.Components
{
    public class BalanceSummary : ViewComponent
    {
        private ICitizenUserRepository citizenUserRepository;

        public BalanceSummary(ICitizenUserRepository citizenUserRepository)
        {
            this.citizenUserRepository = citizenUserRepository;
        }

        public IViewComponentResult Invoke()
        {
            decimal userBalance = 0;

            if (User.Identity.IsAuthenticated)
            {
                var userLogin = User.Identity.Name;
                var user = citizenUserRepository.GetUserByLogin(userLogin);
                userBalance = user.Balance;
            }
            
            return View(userBalance);
        }
    }
}
