using System;

namespace SmartCity.Data.Entities.Medicine
{
    public class MedicineCertificate : BaseModel
    {
        public virtual string Position { get; set; }
        public virtual DateTime DateOfIssue { get; set; }
        public virtual DateTime DateExpiration { get; set; }
        public virtual long UserId { get; set; }
        public virtual CitizenUser User { get; set; }
    }
}
