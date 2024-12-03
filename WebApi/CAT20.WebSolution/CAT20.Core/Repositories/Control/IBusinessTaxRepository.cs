using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;

namespace CAT20.Core.Repositories.Control
{
    public interface IBusinessTaxRepository : IRepository<BusinessTaxes>
    {
        //Task<IEnumerable<BusinessTaxes>> GetAllAsync();
        Task<BusinessTaxes> GetByIdAsync(int id);
        Task<IEnumerable<BusinessTaxes>> GetBusinessTaxForBusinessIdAndYearAsync(int id, int year);
        Task<IEnumerable<BusinessTaxes>> GetBusinessTaxForBusinessIdAsync(int id);

        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForMOHPendingApproval(int sabha);
        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForSecretaryPendingApproval(int sabha);
        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForChairmanPendingApproval(int sabha);

        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForMOHApproved(int sabha);
        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForSecretaryApproved(int sabha);
        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForChairmanApproved(int sabha);

        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForMOHDisapproved(int sabha);
        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForSecretaryDisapproved(int sabha);
        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForChairmanDisapproved(int sabha);

        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForSabhaAndTaxStatus(int sabhaId, TaxStatus taxStatus);

        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForAllApproved(int sabha);
        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForDisapprovedAny(int sabha); 
        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForApprovalPendingAny(int sabha);

        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdApprovalPendingAny(int SabhaId, int officerId);

        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdMOHPendingApproval(int sabha, int officerId);
        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdSecretaryPendingApproval(int sabha, int officerId);
        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdChairmanPendingApproval(int sabha, int officerId);

        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdMOHApproved(int sabha, int officerId);
        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdSecretaryApproved(int sabha, int officerId);
        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdChairmanApproved(int sabha, int officerId);

        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdMOHDisapproved(int sabha, int officerId);
        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdSecretaryDisapproved(int sabha, int officerId);
        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdChairmanDisapproved(int sabha, int officerId);

        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdSabhaAndTaxStatus(int sabhaId, TaxStatus taxStatus, int officerId);

        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdAllApproved(int sabha, int officerId);
        Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdDisapprovedAny(int sabha, int officerId);

        Task<IEnumerable<BusinessTaxes>> GetAllBusinessAndIndustrialTaxesForSabhaAndOfficerAndStatus(int sabhaId, TaxStatus taxStatus, int officerid);
        Task<IEnumerable<BusinessTaxes>> GetAllBusinessAndIndustrialTaxesForSabhaAndStatus(int sabhaId, TaxStatus taxStatus);
       
        //Task<BusinessTaxes> GetByRegNoAndOfficeAsync(string RegNo, int officeId);
        //Task<BusinessTaxes> GetByApplicationNoAsync(string ApplicationNo);
        //Task<BusinessTaxes> GetByApplicationNoAndOfficeAsync(string ApplicationNo, int officeId);
        //Task<IEnumerable<BusinessTaxes>> GetAllForBusinessTaxesSubNatureAndSabhaAsync(int SubNatureId, int sabhaId);
        //Task<IEnumerable<BusinessTaxes>> GetAllForBusinessTaxesNatureAndSabhaAsync(int NatureId, int sabhaId);
        //Task<IEnumerable<BusinessTaxes>> GetAllForSabhaAsync(int sabhaId);
        //Task<IEnumerable<BusinessTaxes>> GetAllForBusinessTaxesOwnerIdAsync(int BusinessTaxesOwnerId);
    }
}
