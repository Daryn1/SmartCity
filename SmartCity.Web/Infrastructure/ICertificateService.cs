using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartCity.Services.Infrastructure;
using SmartCity.Web.Models.Certificates;

namespace SmartCity.Web.Infrastructure
{
    public interface ICertificateService
    {
        Task<OperationResult> IssueCertificate(string certificateName, string userLogin, string issuedBy,
            string description, TimeSpan validityPeriod);

        Task<OperationResult> RevokeCertificate(string certificateName, string userLogin);
        Task<List<CertificateViewModel>> GetCertificatesAsync();
        Task<List<CertificateViewModel>> GetUserCertificates(string userLogin);
        Task<List<CertificateViewModel>> GetCertificatesByName(string certificateName);
        Task<CertificateViewModel> GetCertificateAsync(long certificateId);
        Task<OperationResult> CreateCertificateAsync(CertificateViewModel certificate);
        Task<OperationResult> UpdateCertificateAsync(CertificateViewModel certificate);
        Task<OperationResult> DeleteCertificateAsync(long certificateId);
    }
}