using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCity.Web.Models.Friendships;

namespace SmartCity.Web.Models.Friends
{
    public class FoundUserViewModel
    {
        public string Login { get; set; }

        public string AvatarUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public FriendshipViewModel Friendship { get; set; }
    }
}
