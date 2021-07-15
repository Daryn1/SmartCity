using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCity.Services.Interfaces;
using SmartCity.Web.Models.Friendships;
using SmartCity.Web.Models.Users;

namespace SmartCity.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private IUserService userService;

        private IFriendshipService friendshipService;

        private IMapper mapper;

        public UsersController(IUserService userService, IFriendshipService friendshipService, IMapper mapper)
        {
            this.userService = userService;
            this.friendshipService = friendshipService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("Users/{userLogin}")]
        public IActionResult Profile(string userLogin)
        {
            var user = userService.FindByLogin(userLogin);
            var currentUserLogin = User.Identity.Name;
            var friendship = friendshipService.GetFriendshipByUserLogins(currentUserLogin, userLogin);
            var profileViewModel = mapper.Map<ProfileViewModel>(user);
            var friendshipViewModel = mapper.Map<FriendshipViewModel>(friendship);
            profileViewModel.Friendship = friendshipViewModel;

            return View(profileViewModel);
        }
    }
}
