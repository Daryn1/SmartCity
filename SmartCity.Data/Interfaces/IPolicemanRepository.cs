using System.Collections.Generic;
using SmartCity.Data.Entities;
using SmartCity.Data.Entities.Police;

namespace SmartCity.Data.Interfaces
{
    public interface IPolicemanRepository
    {
        Policeman GetPolicemanByLogin(string userLogin);
        bool IsUserPoliceman(CitizenUser user, out Policeman output);
        bool IsUserPoliceman(string userLogin, out Policeman output);
        void MakePolicemanFromUser(CitizenUser user);
        Policeman Get(long id);
        List<Policeman> GetAll();
        void Save(Policeman model);
        void Delete(long id);
    }
}