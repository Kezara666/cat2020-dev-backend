using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IBusinessTaxService
    {
        //Task<IEnumerable<BusinessTaxes>> GetAll();
        Task<BusinessTaxes> GetById(int id);
        Task<IEnumerable<BusinessTaxes>> GetBusinessTaxForBusinessIdAndYear(int id, int year);
        Task<IEnumerable<BusinessTaxes>> GetBusinessTaxForBusinessId(int id);
        //Task<BusinessTaxes> GetByRegNo(string RegNo);
        //Task<BusinessTaxes> GetByRegNoAndOffice(string RegNo, int officeId);
        //Task<BusinessTaxes> GetByApplicationNo(string ApplicationNo);
        //Task<BusinessTaxes> GetByApplicationNoAndOffice(string ApplicationNo, int officeId);
        Task<BusinessTaxes> Create(BusinessTaxes newBusinessTax);
        Task Update(BusinessTaxes businessToBeUpdated, BusinessTaxes business);
        Task Delete(BusinessTaxes business);

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
        //Task<IEnumerable<BusinessTaxes>> GetAllForSabha(int sabha);
        //Task<IEnumerable<BusinessTaxes>> GetAllForBusinessTaxOwnerId(int businessownerid);
        //Task<IEnumerable<BusinessTaxes>> GetAllForBusinessTaxNatureAndSabha(int id, int sabhaId);
        //Task<IEnumerable<BusinessTaxes>> GetAllForBusinessTaxSubNatureAndSabha(int id, int sabhaId);
    }
}

