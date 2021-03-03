using System;
using SmartCity.Common.Enums;

namespace SmartCity.Data.Entities.UserAccount
{
    public class Certificate : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string IssuedBy { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public CertificateStatus Status { get; set; }

        public virtual CitizenUser Owner { get; set; }
    }
}
