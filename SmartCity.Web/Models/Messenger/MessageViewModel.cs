using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartCity.Web.Models.Messenger
{
    public class MessageViewModel
    {
        [Required]
        public string Date { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string SenderLogin { get; set; }

        [Required]
        public string RecipientLogin { get; set; }
    }
}
