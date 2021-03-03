using System;
using System.ComponentModel.DataAnnotations;

namespace SmartCity.Data.Entities.UserAccount
{
    public class Message : BaseModel
    {
        [Required]
        public virtual DateTime Date { get; set; }

        [Required]
        public virtual string Text { get; set; }

        [Required]
        public virtual CitizenUser Sender { get; set; }

        [Required]
        public virtual CitizenUser Recipient { get; set; }
    }
}
