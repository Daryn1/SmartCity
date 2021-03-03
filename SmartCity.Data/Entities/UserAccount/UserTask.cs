using System;
using System.ComponentModel.DataAnnotations;
using SmartCity.Common.Enums;

namespace SmartCity.Data.Entities.UserAccount
{
    public class UserTask : BaseModel
    {
        [Required]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        [Required]
        public virtual DateTime StartDate { get; set; }

        [Required]
        public virtual DateTime EndDate { get; set; }

        public virtual TaskStatus Status { get; set; }

        public virtual TaskPriority Priority { get; set; }

        public virtual CitizenUser Owner { get; set; }
    }
}
