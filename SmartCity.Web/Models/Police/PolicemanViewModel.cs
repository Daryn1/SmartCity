using System;
using SmartCity.Common.Enums;
using SmartCity.Web.Models.Account;

namespace SmartCity.Web.Models.Police
{
    public class PolicemanViewModel
    {
        public MyProfileViewModel ProfileVM { get; set; }

        public PolicemanRank? Rank { get; set; } = null;

        public string RankString
        {
            get
            {
                return Rank switch
                {
                    PolicemanRank.NotVerified => "Гражданин",
                    PolicemanRank.Policeman => "Полицейский",
                    PolicemanRank.MorgueEmployee => "Работник морга",
                    null => "Не верифицирован",
                    _ => throw new NotImplementedException()
                };
            }
        }
    }
}
