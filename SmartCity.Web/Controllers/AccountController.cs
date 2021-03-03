using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SmartCity.Data.Entities;
using SmartCity.Data.Interfaces;
using SmartCity.Services.Interfaces;
using SmartCity.Web.Models.Account;

namespace SmartCity.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IUserService userService;
        private readonly IAdressRepository adressRepository;
        private readonly IMapper mapper;

        public AccountController(IWebHostEnvironment hostEnvironment, IUserService userService, IAdressRepository adressRepository, IMapper mapper)
        {
            this.hostEnvironment = hostEnvironment;
            this.userService = userService;
            this.adressRepository = adressRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var viewModel = new LoginViewModel();

            viewModel.ReturnUrl = Request.Query["ReturnUrl"];

            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = userService.GetUserByNameAndPassword(loginViewModel.Login, loginViewModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Login or password is incorrect.");
                    return View(loginViewModel);
                }

                var claimsIdentity = new ClaimsIdentity(Startup.AuthMethod);
                claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Login));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.AuthenticationMethod, Startup.AuthMethod));

                foreach (var role in user.Roles)
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
                }

                var userPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(userPrincipal, new AuthenticationProperties { IsPersistent = loginViewModel.IsPersistent });

                if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }

                return Redirect(loginViewModel.ReturnUrl);
            }

            return View(loginViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View(new RegistrationViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Registration(RegistrationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = mapper.Map<CitizenUser>(viewModel);
            userService.Save(user);

            return RedirectToAction(nameof(Login));
        }

        public IActionResult MyProfile()
        {
            var user = userService.GetCurrentUser();
            var viewModel = mapper.Map<MyProfileViewModel>(user);

            return View(viewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAvatar(IFormFile avatar)
        {
            var user = userService.GetCurrentUser();
            var fileName = user.Login;
            var wwwrootPath = hostEnvironment.WebRootPath;
            var path = @$"{wwwrootPath}\image\avatar\{fileName}.jpg";
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await avatar.CopyToAsync(fileStream);
            }

            user.AvatarUrl = $"/image/avatar/{fileName}.jpg";
            userService.Save(user);

            return RedirectToAction("MyProfile");
        }

        [HttpPost]
        public IActionResult SaveAddress(AdressViewModel addressViewModel)
        {
            var address = mapper.Map<Adress>(addressViewModel);
            var user = userService.FindByLogin(addressViewModel.OwnerLogin);
            address.Owner = user;
            adressRepository.Save(address);

            return RedirectToAction("MyProfile");
        }
    }
}
