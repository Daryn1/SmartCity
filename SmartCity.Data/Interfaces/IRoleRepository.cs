using System.Collections.Generic;
using SmartCity.Data.Entities.UserAccount;

namespace SmartCity.Data.Interfaces
{
    public interface IRoleRepository
    {
        Role GetRoleByName(string roleName);
        bool RoleExists(string roleName);
        Role Get(long id);
        List<Role> GetAll();
        void Save(Role model);
        void Delete(long id);
    }
}