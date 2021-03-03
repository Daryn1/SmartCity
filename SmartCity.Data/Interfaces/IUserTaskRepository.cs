using System.Collections.Generic;
using System.Linq;
using SmartCity.Data.Entities.UserAccount;

namespace SmartCity.Data.Interfaces
{
    public interface IUserTaskRepository
    {
        IQueryable<UserTask> GetTasksByUserLogin(string userLogin);
        bool TaskExists(long taskId);
        bool TaskWithNameExists(string taskName);
        UserTask Get(long id);
        List<UserTask> GetAll();
        void Save(UserTask model);
        void Delete(long id);
    }
}