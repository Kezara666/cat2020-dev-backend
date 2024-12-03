using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IBusinessService
    {
        Task<IEnumerable<Business>> GetAll();
        Task<Business> GetById(int id);
        Task<Business> GetByRegNo(string RegNo);
        Task<Business> GetByRegNoAndOffice(string RegNo, int officeId);
        Task<Business> GetByApplicationNo(string ApplicationNo);
        Task<Business> GetByApplicationNoAndOffice(string ApplicationNo, int officeId);
        Task<Business> CreateBusiness(Business newBusiness);
        Task Update(Business businessToBeUpdated, Business business);
        Task Delete(Business business);
        Task<IEnumerable<Business>> GetAllForSabha(int sabha);
        Task<IEnumerable<Business>> GetAllForBusinessOwnerId(int businessownerid);
        Task<IEnumerable<Business>> GetAllBusinessAndIndustrialTaxesForOwnerId(int businessownerid);
        Task<IEnumerable<Business>> GetAllBusinessAndIndustrialTaxesForOwnerIdAndSabhaId(int businessownerid, int sabhaid);
        Task<IEnumerable<Business>> GetAllForBusinessNatureAndSabha(int id, int sabhaId);
        Task<IEnumerable<Business>> GetAllForBusinessSubNatureAndSabha(int id, int sabhaId);
        Task<IEnumerable<Business>> GetAllBusinessAndIndustrialTaxesForSabha(int sabha);
        Task<IEnumerable<Business>> GetAllBusinessLicensesForSabha(int sabha);
        Task<IEnumerable<Business>> GetAllBusinessAndIndustrialTaxesForSabhaAndOfficer(int sabha, int officerid);
        Task<IEnumerable<Business>> GetAllBusinessLicensesForSabhaAndOfficer(int sabha, int officerid);

    }
}

