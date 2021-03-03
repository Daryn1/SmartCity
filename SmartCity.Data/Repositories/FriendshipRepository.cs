using System.Linq;
using SmartCity.Data.Entities.UserAccount;
using SmartCity.Data.Interfaces;

namespace SmartCity.Data.Repositories
{
    public class FriendshipRepository : BaseRepository<Friendship>, IFriendshipRepository
    {
        public FriendshipRepository(SmartCityDbContext context) : base(context)
        {
        }

        public bool FriendshipExists(long friendshipId)
        {
            return dbSet.Any(friendship => friendship.Id == friendshipId);
        }

        public bool FriendshipBetweenUsersExists(string userLogin1, string userLogin2)
        {
            return dbSet.Any(friendship => friendship.Requester.Login == userLogin1 && friendship.Requested.Login == userLogin2
                                           || friendship.Requester.Login == userLogin2 && friendship.Requested.Login == userLogin1);
        }

        public Friendship GetFriendshipByUserLogins(string userLogin1, string userLogin2)
        {
            return dbSet.SingleOrDefault(friendship => friendship.Requester.Login == userLogin1 && friendship.Requested.Login == userLogin2
                                                       || friendship.Requester.Login == userLogin2 && friendship.Requested.Login == userLogin1);
        }
    }
}
