using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using SmartCity.Data.Entities;
using SmartCity.Data.Interfaces;
using SmartCity.Services.Infrastructure;
using SmartCity.Services.Interfaces;

namespace SmartCity.Services.Services
{
    public class UserService : IUserService
    {
        private ICitizenUserRepository citizenUserRepository;
        private IRoleRepository roleRepository;
        private IHttpContextAccessor httpContextAccessor;

        public UserService(ICitizenUserRepository citizenUserRepository, IRoleRepository roleRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            this.citizenUserRepository = citizenUserRepository;
            this.roleRepository = roleRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public CitizenUser GetCurrentUser()
        {
            var idStr = httpContextAccessor.HttpContext.
                User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(idStr))
            {
                return null;
            }

            var id = int.Parse(idStr);
            var user = citizenUserRepository.Get(id);
            return user;
        }

        public List<CitizenUser> GetUsers()
        {
            return citizenUserRepository.GetAll();
        }

        public CitizenUser FindById(long id)
        {
            return citizenUserRepository.Get(id);
        }

        public CitizenUser FindByLogin(string login)
        {
            return citizenUserRepository.GetUserByLogin(login);
        }

        public bool UserExists(string login)
        {
            return citizenUserRepository.UserExists(login);
        }

        public void Save(CitizenUser user)
        {
            citizenUserRepository.Save(user);
        }

        public void Delete(CitizenUser user)
        {
            citizenUserRepository.Delete(user.Id);
        }

        public async Task<OperationResult> SignInAsync(string userName, string password, bool isPersistent)
        {
            var user = citizenUserRepository.GetUserByNameAndPassword(userName, password);

            if (user == null)
            {
                return OperationResult.Failed("Login or password is incorrect.");
            }

            var claimsIdentity = new ClaimsIdentity("CookieAuth");
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Login));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.AuthenticationMethod, "CookieAuth"));

            foreach (var role in user.Roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
            }

            var userPrincipal = new ClaimsPrincipal(claimsIdentity);

            await httpContextAccessor.HttpContext.Authentication.SignInAsync("CookieAuth", userPrincipal, new AuthenticationProperties { IsPersistent = isPersistent });

            return OperationResult.Success();
        }

        public CitizenUser GetUserByLogin(string userLogin)
        {
            return citizenUserRepository.GetUserByLogin(userLogin);
        }

        public bool IsInRole(CitizenUser user, string roleName)
        {
            return user.Roles.Any() && user.Roles.All(useRole => useRole.Name == roleName);
        }

        public OperationResult AddToRole(CitizenUser user, string roleName)
        {
            if (user == null)
            {
                return OperationResult.Failed("Specified user does not exist.");
            }

            var role = roleRepository.GetRoleByName(roleName);

            if (role == null)
            {
                return OperationResult.Failed("Specified role does not exist.");
            }

            if (IsInRole(user, roleName))
            {
                return OperationResult.Failed($"User {user.Login} is already in role = {roleName}");
            }

            user.Roles.Add(role);
            citizenUserRepository.Save(user);
            return OperationResult.Success();
        }

        public OperationResult RemoveFromRole(CitizenUser user, string roleName)
        {
            if (user == null)
            {
                return OperationResult.Failed("Specified user does not exist.");
            }

            var role = roleRepository.GetRoleByName(roleName);

            if (role == null)
            {
                return OperationResult.Failed("Specified role does not exist.");
            }

            if (!IsInRole(user, roleName))
            {
                return OperationResult.Failed($"User {user.Login} is not in role = {roleName}");
            }

            user.Roles.Remove(role);
            citizenUserRepository.Save(user);
            return OperationResult.Success();
        }

        public List<CitizenUser> GetBlockedUsers()
        {
            return citizenUserRepository.GetBlockedUsers().ToList();
        }

        public List<CitizenUser> SearchUsers(string searchTerm)
        {
            var usersAsQueryable = citizenUserRepository.GetUsersAsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                usersAsQueryable = usersAsQueryable.Where(user =>
                        user.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                        user.LastName.ToLower().Contains(searchTerm.ToLower()));
            }

            var foundUsers = usersAsQueryable.Take(10).OrderBy(user => user.FirstName).ToList();

            return foundUsers;
        }

        public CitizenUser GetUserByNameAndPassword(string userLogin, string password)
        {
            return citizenUserRepository.GetUserByNameAndPassword(userLogin, password);
        }
    }
}
