using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCity.Services.Interfaces;

namespace SmartCity.Web.Controllers.CustomAttribute
{
    public class IsAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userService = context.HttpContext.RequestServices
                .GetService(typeof(IUserService)) as IUserService;

            if (userService.GetCurrentUser()?.Login.ToLower() != "admin")
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
