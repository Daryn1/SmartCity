using System.Linq;
using SmartCity.Data.Entities.Medicine;
using SmartCity.Data.Interfaces;
using Microsoft.AspNetCore.Http;

namespace SmartCity.Services.Services
{
    public class InsuranceService
    {
        private IMedicalInsuranceRepository insuranceRepository;

        private IHttpContextAccessor httpContextAccessor;

        public InsuranceService(IMedicalInsuranceRepository insuranceRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.insuranceRepository = insuranceRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public MedicalInsurance GetCurrentUserInsurance()
        {
            var idStr = httpContextAccessor.HttpContext.
                User.Claims.SingleOrDefault(x => x.Type == "Id")?.Value;
            if(string.IsNullOrEmpty(idStr))
            {
                return null;
            }

            var id = int.Parse(idStr);
            var insurance = insuranceRepository.Get(id);

            return insurance;
        }
    }
}
