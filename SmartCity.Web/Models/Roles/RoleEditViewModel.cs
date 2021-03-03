using System;
using System.Collections.Generic;

namespace SmartCity.Web.Models.Roles
{
    public class RoleEditViewModel
    {
        public RoleViewModel Role { get; set; }

        public List<string> MemberLogins { get; set; }

        public List<string> NonMemberLogins { get; set; }
    }
}
