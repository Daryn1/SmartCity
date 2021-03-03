using System.Collections.Generic;
using SmartCity.Data.Entities.UserAccount;

namespace SmartCity.Services.Interfaces
{
    public interface ITaskService
    {
        List<UserTask> GetUserPlannedTasks(string userLogin);
        List<UserTask> GetUserTasksPlannedForToday(string userLogin);
        List<UserTask> GetUserTasksPlannedForTomorrow(string userLogin);
        List<UserTask> GetUserTasksPlannedForNextDays(string userLogin, int daysFromToday);
        List<UserTask> GetUserCompletedTasks(string userLogin);
        List<UserTask> GetUserOverdueTasks(string userLogin);
        UserTask GetTaskById(long taskId);
        void Save(UserTask task);
        void Delete(long taskId);
        bool Exists(long taskId);
    }
}