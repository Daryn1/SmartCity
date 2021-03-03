using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartCity.Services.Interfaces;
using SmartCity.Web.Models.Friends;

namespace SmartCity.Web.Components
{
    public class RecentChats : ViewComponent
    {
        private IUserService userService;

        private IMapper mapper;

        public RecentChats(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = userService.GetCurrentUser();
                var friendViewModels = mapper.Map<List<FriendViewModel>>(user.Friends);

                return View(friendViewModels);
            }

            return View(new List<FriendViewModel>());
        }
    }
}
