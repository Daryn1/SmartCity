﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartCity.Web.Filters;
using SmartCity.Web.Models.Certificates;
using ICertificateService = SmartCity.Web.Infrastructure.ICertificateService;

namespace SmartCity.Web.Controllers
{
    public class CertificatesController : Controller
    {
        private readonly ICertificateService certificateService;

        public CertificatesController(ICertificateService certificateService)
        {
            this.certificateService = certificateService;
        }

        [ImportModelStateErrorsFromTempData]
        public async Task<IActionResult> Index(string userLogin, string certificateName)
        {
            List<CertificateViewModel> certificates;

            if (!string.IsNullOrWhiteSpace(certificateName))
            {
                certificates = await certificateService.GetCertificatesByName(certificateName);
            }
            else if (!string.IsNullOrWhiteSpace(userLogin))
            {
                certificates = await certificateService.GetUserCertificates(userLogin);
            }
            else
            {
                certificates = await certificateService.GetCertificatesAsync();
            }

            return View(certificates);
        }

        public ViewResult Get()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Get(long id)
        {
            var certificateViewModel = await certificateService.GetCertificateAsync(id);

            return View(certificateViewModel);
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CertificateViewModel certificate)
        {
            if (ModelState.IsValid)
            {
                var operationResult = await certificateService.CreateCertificateAsync(certificate);

                if (!operationResult.Succeeded)
                {
                    operationResult.Errors.ForEach(error => ModelState.AddModelError(string.Empty, error));
                }
            }

            return View(certificate);
        }

        [HttpPost]
        [ExportModelStateErrorsToTempData]
        public async Task<IActionResult> Issue(string userLogin)
        {
            var operationResult = await certificateService.IssueCertificate("Birth Certificate", userLogin,
                "Government", "The certificate documents that the employee is unfit for work", TimeSpan.FromDays(3650));

            if (!operationResult.Succeeded)
            {
                operationResult.Errors.ForEach(error => ModelState.AddModelError(string.Empty, error));
            }

            var urlReferrer = Request.Headers["Referer"].ToString();

            return Redirect(urlReferrer);
        }

        [HttpPost]
        [ExportModelStateErrorsToTempData]
        public async Task<IActionResult> Revoke(string certificateName, string userLogin)
        {
            var operationResult = await certificateService.RevokeCertificate(certificateName, userLogin);

            if (!operationResult.Succeeded)
            {
                operationResult.Errors.ForEach(error => ModelState.AddModelError(string.Empty, error));
            }

            var urlReferrer = Request.Headers["Referer"].ToString();

            return Redirect(urlReferrer);
        }

        public async Task<IActionResult> Update(long id)
        {
            var certificateViewModel = await certificateService.GetCertificateAsync(id);

            return View(certificateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CertificateViewModel certificateViewModel)
        {
            if (ModelState.IsValid)
            {
                var operationResult = await certificateService.UpdateCertificateAsync(certificateViewModel);

                if (!operationResult.Succeeded)
                {
                    operationResult.Errors.ForEach(error => ModelState.AddModelError(string.Empty, error));
                }
            }

            return View(certificateViewModel);
        }

        [HttpPost]
        [ExportModelStateErrorsToTempData]
        public async Task<IActionResult> Delete(long id)
        {
            var operationResult = await certificateService.DeleteCertificateAsync(id);

            if (!operationResult.Succeeded)
            {
                operationResult.Errors.ForEach(error => ModelState.AddModelError(string.Empty, error));
            }

            var urlReferrer = Request.Headers["Referer"].ToString();

            return Redirect(urlReferrer);
        }
    }
}
