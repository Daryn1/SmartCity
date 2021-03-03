using System;
using System.ComponentModel.DataAnnotations;
using SmartCity.Common.Enums;

namespace SmartCity.Data.Entities.UserAccount
{
    public class Friendship : BaseModel
    {
        [Required]
        public virtual DateTime RequestDate { get; set; }

        public virtual DateTime? AcceptanceDate { get; set; }

        [Required]
        public virtual FriendshipStatus FriendshipStatus { get; set; }

        [Required]
        public virtual CitizenUser Requester { get; set; }

        [Required]
        public virtual CitizenUser Requested { get; set; }
    }
}
