using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCity.Data.Interfaces;
using SmartCity.Services.Interfaces;
using SmartCity.Web.Models.HDDoctor;

namespace SmartCity.Web.Controllers
{
    [Authorize(Roles = "Doctor")]

    public class HDDoctorPageController : Controller
    {
        private IMapper mapper;
        private ICitizenUserRepository citizenRepository;
        private IUserService userService;

        public HDDoctorPageController(IMapper mapper, ICitizenUserRepository citizenRepository, IUserService userService)
        {
            this.mapper = mapper;
            this.citizenRepository = citizenRepository;
            this.userService = userService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult DoctorPage()
        {
            var user = userService.GetCurrentUser();
            var viewModel = mapper.Map<DoctorPageViewModel>(user);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DoctorPage(DoctorPageViewModel viewModel)
        {
            
            return View(viewModel);
        }

    }
}
