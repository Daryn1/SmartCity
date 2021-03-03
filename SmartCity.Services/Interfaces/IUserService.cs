using System.Collections.Generic;
using System.Threading.Tasks;
using SmartCity.Data.Entities;
using SmartCity.Services.Infrastructure;

namespace SmartCity.Services.Interfaces
{
    public interface IUserService
    {
        CitizenUser GetCurrentUser();
        List<CitizenUser> GetUsers();
        CitizenUser FindById(long id);
        CitizenUser FindByLogin(string login);
        bool UserExists(string login);
        void Save(CitizenUser user);
        void Delete(CitizenUser user);
        Task<OperationResult> SignInAsync(string userName, string password, bool isPersistent);
        CitizenUser GetUserByLogin(string userLogin);
        bool IsInRole(CitizenUser user, string roleName);
        OperationResult AddToRole(CitizenUser user, string roleName);
        OperationResult RemoveFromRole(CitizenUser user, string roleName);
        List<CitizenUser> GetBlockedUsers();
        List<CitizenUser> SearchUsers(string searchTerm);
        CitizenUser GetUserByNameAndPassword(string userLogin, string password);
    }
}