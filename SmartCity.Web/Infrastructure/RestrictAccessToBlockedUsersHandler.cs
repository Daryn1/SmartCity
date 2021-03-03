using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SmartCity.Data.Interfaces;

namespace SmartCity.Web.Infrastructure
{
    public class RestrictAccessToBlockedUsersHandler : AuthorizationHandler<RestrictAccessToBlockedUsersRequirement>
    {
        private ICitizenUserRepository citizenUserRepository;

        public RestrictAccessToBlockedUsersHandler(ICitizenUserRepository citizenUserRepository)
        {
            this.citizenUserRepository = citizenUserRepository;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RestrictAccessToBlockedUsersRequirement requirement)
        {
            var currentUserLogin = context.User.Identity?.Name;

            if (string.IsNullOrEmpty(currentUserLogin))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var currentUser = citizenUserRepository.GetUserByLogin(currentUserLogin);

            if (currentUser.IsBlocked)
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class RestrictAccessToBlockedUsersRequirement : IAuthorizationRequirement
    {
    }
}
