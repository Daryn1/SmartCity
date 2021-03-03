using System.Linq;
using SmartCity.Data.Entities.UserAccount;
using SmartCity.Data.Interfaces;

namespace SmartCity.Data.Repositories
{
    public class UserTaskRepository : BaseRepository<UserTask>, IUserTaskRepository
    {
        public UserTaskRepository(SmartCityDbContext context) : base(context)
        {
        }

        public IQueryable<UserTask> GetTasksByUserLogin(string userLogin)
        {
            return dbSet.Where(userTask => userTask.Owner.Login == userLogin);
        }

        public bool TaskExists(long taskId)
        {
            return dbSet.Any(task => task.Id == taskId);
        }

        public bool TaskWithNameExists(string taskName)
        {
            return dbSet.Any(task => task.Name == taskName);
        }
    }
}
