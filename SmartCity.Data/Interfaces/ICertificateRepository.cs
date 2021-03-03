using System.Collections.Generic;
using System.Threading.Tasks;
using SmartCity.Data.Entities.UserAccount;

namespace SmartCity.Data.Interfaces
{
    public interface ICertificateRepository
    {
        Task SaveAsync(Certificate certificate);
        Task<bool> DoesCitizenAlreadyHaveValidCertificateAsync(Certificate certificate);
        Task<List<Certificate>> GetCertificatesByNameAsync(string certificateName);
        Task<List<Certificate>> GetUserCertificatesAsync(string userLogin);
        Task<Certificate> GetByIdAsync(long id);
        Task<List<Certificate>> GetAllAsync();
        Task DeleteAsync(long id);
        Task<bool> Exists(long id);
    }
}