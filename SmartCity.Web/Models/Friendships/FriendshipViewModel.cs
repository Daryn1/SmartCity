using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SmartCity.Common.Enums;

namespace SmartCity.Web.Models.Friendships
{
    public class FriendshipViewModel
    {
        [Range(0, long.MaxValue)]
        public long Id { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime? AcceptanceDate { get; set; }

        public FriendshipStatus FriendshipStatus { get; set; }

        public string RequesterLogin { get; set; }

        public string RequestedLogin { get; set; }
    }
}
