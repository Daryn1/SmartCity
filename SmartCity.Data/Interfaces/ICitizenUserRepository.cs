using System.Collections.Generic;
using System.Linq;
using SmartCity.Data.Entities;

namespace SmartCity.Data.Interfaces
{
    public interface ICitizenUserRepository
    {
        IQueryable<CitizenUser> GetUsersAsQueryable();
        IEnumerable<CitizenUser> GetUserWithHome();
        CitizenUser GetUserByLogin(string userLogin);
        bool UserExists(string userLogin);
        CitizenUser GetUserByNameAndPassword(string userName, string password);
        CitizenUser GetUserByPassword(string password);
        IEnumerable<CitizenUser> GetFamiliarUserNames(string userName);
        IQueryable<CitizenUser> GetUsersByLogins(List<string> userLogins);
        IQueryable<CitizenUser> GetBlockedUsers();
        IQueryable<CitizenUser> GetDeadUsers();
        IQueryable<CitizenUser> GetUsersWithCertificate(string certificateName);
        CitizenUser Get(long id);
        List<CitizenUser> GetAll();
        void Save(CitizenUser model);
        void Delete(long id);
    }
}