using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCity.Web.Models.Friends;

namespace SmartCity.Web.Models.Messenger
{
    public class StartedChatViewModel
    {
        public FriendViewModel Friend { get; set; }

        public MessageViewModel LastMessage { get; set; }
    }
}
