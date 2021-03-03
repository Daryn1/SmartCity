using System.Collections.Generic;
using SmartCity.Data.Entities.UserAccount;

namespace SmartCity.Data.Interfaces
{
    public interface IFriendshipRepository
    {
        bool FriendshipExists(long friendshipId);
        bool FriendshipBetweenUsersExists(string userLogin1, string userLogin2);
        Friendship GetFriendshipByUserLogins(string userLogin1, string userLogin2);
        Friendship Get(long id);
        List<Friendship> GetAll();
        void Save(Friendship model);
        void Delete(long id);
    }
}