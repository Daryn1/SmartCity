using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCity.Common.Enums;
using SmartCity.Web.Models.Account;
using SmartCity.Web.Models.Certificates;
using SmartCity.Web.Models.Friends;
using SmartCity.Web.Models.Friendships;

namespace SmartCity.Web.Models.Users
{
    public class ProfileViewModel
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string AvatarUrl { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime LastLoginDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsMarried { get; set; }

        public FriendshipViewModel Friendship { get; set; }

        public List<FriendViewModel> Friends { get; set; }

        public List<AdressViewModel> Adresses { get; set; }

        public List<CertificateViewModel> Certificates { get; set; }
    }
}
