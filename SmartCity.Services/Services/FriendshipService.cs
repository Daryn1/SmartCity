using System;
using SmartCity.Common.Enums;
using SmartCity.Data.Entities.UserAccount;
using SmartCity.Data.Interfaces;
using SmartCity.Services.Infrastructure;
using SmartCity.Services.Interfaces;

namespace SmartCity.Services.Services
{
    public class FriendshipService : IFriendshipService
    {
        private IFriendshipRepository friendshipRepository;

        private ICitizenUserRepository citizenUserRepository;

        public FriendshipService(IFriendshipRepository friendshipRepository, ICitizenUserRepository citizenUserRepository)
        {
            this.friendshipRepository = friendshipRepository;
            this.citizenUserRepository = citizenUserRepository;
        }

        public Friendship GetFriendshipByUserLogins(string userLogin1, string userLogin2)
        {
            return friendshipRepository.GetFriendshipByUserLogins(userLogin1, userLogin2);
        }

        public OperationResult CreateFriendRequest(string requesterLogin, string requestedLogin)
        {
            var requester = citizenUserRepository.GetUserByLogin(requesterLogin);

            if (requester == null)
            {
                return OperationResult.Failed($"User with Login = {requesterLogin} not found");
            }

            var requested = citizenUserRepository.GetUserByLogin(requestedLogin);

            if (requested == null)
            {
                return OperationResult.Failed($"User with Login = {requestedLogin} not found");
            }

            if (friendshipRepository.FriendshipBetweenUsersExists(requesterLogin, requestedLogin))
            {
                return OperationResult.Failed("Friendship between users already exists");
            }

            var friendRequest = new Friendship
            {
                RequestDate = DateTime.Now,
                AcceptanceDate = null,
                FriendshipStatus = FriendshipStatus.Pending,
                Requester = requester,
                Requested = requested
            };

            friendshipRepository.Save(friendRequest);

            return OperationResult.Success();
        }

        public OperationResult AcceptFriendRequest(long friendshipId)
        {
            var friendship = friendshipRepository.Get(friendshipId);

            if (friendship == null)
            {
                return OperationResult.Failed($"FriendRequest with ID = {friendshipId} does not exist");
            }

            friendship.FriendshipStatus = FriendshipStatus.Accepted;
            friendshipRepository.Save(friendship);

            return OperationResult.Success();
        }

        public OperationResult DeclineFriendRequest(long friendshipId)
        {
            var friendship = friendshipRepository.Get(friendshipId);

            if (friendship == null)
            {
                return OperationResult.Failed($"FriendRequest with ID = {friendshipId} does not exist");
            }

            friendship.FriendshipStatus = FriendshipStatus.Declined;
            friendshipRepository.Save(friendship);

            return OperationResult.Success();
        }

        public OperationResult CancelFriendRequest(long friendshipId)
        {
            if (!friendshipRepository.FriendshipExists(friendshipId))
            {
                return OperationResult.Failed($"FriendRequest with ID = {friendshipId} does not exist");
            }

            friendshipRepository.Delete(friendshipId);

            return OperationResult.Success();
        }

        public OperationResult Unfriend(string userLogin1, string userLogin2)
        {
            var friendship = friendshipRepository.GetFriendshipByUserLogins(userLogin1, userLogin2);

            if (friendship == null || friendship.FriendshipStatus != FriendshipStatus.Accepted)
            {
                return OperationResult.Failed($"Users with logins {userLogin1} and {userLogin2} are not friends");
            }

            friendshipRepository.Delete(friendship.Id);

            return OperationResult.Success();
        }
    }
}
