using SmartCity.Data.Entities.UserAccount;
using SmartCity.Services.Infrastructure;

namespace SmartCity.Services.Interfaces
{
    public interface IFriendshipService
    {
        Friendship GetFriendshipByUserLogins(string userLogin1, string userLogin2);
        OperationResult CreateFriendRequest(string requesterLogin, string requestedLogin);
        OperationResult AcceptFriendRequest(long friendshipId);
        OperationResult DeclineFriendRequest(long friendshipId);
        OperationResult CancelFriendRequest(long friendshipId);
        OperationResult Unfriend(string userLogin1, string userLogin2);
    }
}