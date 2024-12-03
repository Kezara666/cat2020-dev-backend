using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;

namespace CAT20.Core.Repositories.Control
{
    public interface IBusinessRepository : IRepository<Business>
    {
        Task<IEnumerable<Business>> GetAllAsync();
        Task<Business> GetByIdAsync(int id);
        Task<Business> GetByRegNoAsync(string RegNo);
        Task<Business> GetByRegNoAndOfficeAsync(string RegNo, int officeId);
        Task<Business> GetByApplicationNoAsync(string ApplicationNo);
        Task<Business> GetByApplicationNoAndOfficeAsync(string ApplicationNo, int officeId);
        Task<IEnumerable<Business>> GetAllForBusinessSubNatureAndSabhaAsync(int SubNatureId, int sabhaId);
        Task<IEnumerable<Business>> GetAllForBusinessNatureAndSabhaAsync(int NatureId, int sabhaId);
        Task<IEnumerable<Business>> GetAllForSabhaAsync(int sabhaId);
        Task<IEnumerable<Business>> GetAllForBusinessOwnerIdAsync(int BusinessOwnerId);
        Task<IEnumerable<Business>> GetAllBusinessAndIndustrialTaxesForOwnerId(int businessownerid);
        Task<IEnumerable<Business>> GetAllBusinessAndIndustrialTaxesForOwnerIdAndSabhaId(int businessownerid, int sabhaid);
        Task<IEnumerable<Business>> GetAllBusinessAndIndustrialTaxesForSabha(int sabhaId);
        Task<IEnumerable<Business>> GetAllBusinessLicensesForSabha(int sabhaId);
        Task<IEnumerable<Business>> GetAllBusinessAndIndustrialTaxesForSabhaAndOfficer(int sabha, int officerid);
        Task<IEnumerable<Business>> GetAllBusinessLicensesForSabhaAndOfficer(int sabha, int officerid);

    }
}
