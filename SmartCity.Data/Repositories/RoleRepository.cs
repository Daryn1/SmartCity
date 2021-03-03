using System.Linq;
using SmartCity.Data.Entities.UserAccount;
using SmartCity.Data.Interfaces;

namespace SmartCity.Data.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(SmartCityDbContext context) : base(context)
        {
        }

        public Role GetRoleByName(string roleName)
        {
            return dbSet.SingleOrDefault(role => role.Name == roleName);
        }

        public bool RoleExists(string roleName)
        {
            return GetRoleByName(roleName) != null;
        }
    }
}
