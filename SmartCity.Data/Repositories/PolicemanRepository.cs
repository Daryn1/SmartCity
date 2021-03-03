using System.Linq;
using SmartCity.Common.Enums;
using SmartCity.Data.Entities;
using SmartCity.Data.Entities.Police;
using SmartCity.Data.Interfaces;

namespace SmartCity.Data.Repositories
{
    public class PolicemanRepository : BaseRepository<Policeman>, IPolicemanRepository
    {
        public PolicemanRepository(SmartCityDbContext context) : base(context) { }

        public Policeman GetPolicemanByLogin(string userLogin)
        {
            return dbSet.SingleOrDefault(u => u.User.Login == userLogin);
        }

        public bool IsUserPoliceman(CitizenUser user, out Policeman output)
        {
            output = dbSet.Where(p => p.User.Login == user.Login).SingleOrDefault();
            return output != null;
        }

        public bool IsUserPoliceman(string userLogin, out Policeman output)
        {
            output = dbSet.Where(p => p.User.Login == userLogin).SingleOrDefault();
            return output != null;
        }

        public void MakePolicemanFromUser(CitizenUser user)
        {
            var policeman = new Policeman() { User = user, Rank = PolicemanRank.NotVerified };
            Save(policeman);
        }
    }
}
