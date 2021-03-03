using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartCity.Common.Enums;

namespace SmartCity.Data.Entities.Police
{
    public class Violation : BaseModel
    {
        public virtual CitizenUser BlamingUser { get; set; }

        [Required]
        public virtual CitizenUser BlamedUser { get; set; }
        
        public virtual Policeman ViewingPoliceman { get; set; }
        
        public DateTime Date { get; set; }
        
        public DateTime? ConfirmDate { get; set; }
        
        public string Explanation { get; set; }

        public string PolicemanCommentary { get; set; }

        public TypeOfOffense OffenseType { get; set; }

        public CurrentStatus Status { get; set; }

        public DateTime? TermOfPunishment { get; set; }

        [Column(TypeName = "money")]
        public decimal? Penalty { get; set; }

    }
}
