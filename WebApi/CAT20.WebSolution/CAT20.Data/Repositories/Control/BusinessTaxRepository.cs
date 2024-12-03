using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Control
{
    public class BusinessTaxRepository : Repository<BusinessTaxes>, IBusinessTaxRepository
    {
        public BusinessTaxRepository(DbContext context) : base(context)
        {
        }
        public async Task<BusinessTaxes> GetByIdAsync(int id)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.BusinessId == id && m.Status == 1)
                .OrderByDescending(m=> m.Id)
                .FirstOrDefaultAsync();
        }
        

        public async Task<IEnumerable<BusinessTaxes>> GetBusinessTaxForBusinessIdAndYearAsync(int businessid, int year)
        {
            var result = await controlDbContext.BusinessTaxes
                .Where(m => m.BusinessId == businessid && m.CurrentYear==year)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BusinessTaxes>> GetBusinessTaxForBusinessIdAsync(int businessid)
        {
            var result = await controlDbContext.BusinessTaxes
                .Where(m => m.BusinessId == businessid && m.Status==1)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForMOHPendingApproval(int sabhaId)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_moh_approved==0)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForSecretaryPendingApproval(int sabhaId)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_secretary_approved == 0)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForChairmanPendingApproval(int sabhaId)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_chairman_approved == 0)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForMOHApproved(int sabhaId)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_moh_approved == 1)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForSecretaryApproved(int sabhaId)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_secretary_approved == 1)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForChairmanApproved(int sabhaId)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_chairman_approved == 1)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForMOHDisapproved(int sabhaId)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_moh_approved == 2)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForSecretaryDisapproved(int sabhaId)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_secretary_approved == 2)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForChairmanDisapproved(int sabhaId)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_chairman_approved == 2)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }
        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForAllApproved(int sabhaId)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_moh_approved == 1 && m.is_secretary_approved == 1 && m.is_chairman_approved == 1)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForDisapprovedAny(int sabhaId)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && (m.is_moh_approved == 2 || m.is_secretary_approved == 2 || m.is_chairman_approved == 2))
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForApprovalPendingAny(int sabhaId)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && (m.is_moh_approved == 0 || m.is_secretary_approved == 0 || m.is_chairman_approved == 0))
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForSabhaAndTaxStatus(int sabhaId, TaxStatus taxStatus)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.TaxState == taxStatus)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdMOHPendingApproval(int sabhaId, int officerid)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_moh_approved == 0 && m.CreatedBy==officerid)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdSecretaryPendingApproval(int sabhaId, int officerid)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_secretary_approved == 0 && m.CreatedBy == officerid)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdChairmanPendingApproval(int sabhaId, int officerid)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_chairman_approved == 0 && m.CreatedBy == officerid)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdMOHApproved(int sabhaId, int officerid)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_moh_approved == 1 && m.CreatedBy == officerid)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdSecretaryApproved(int sabhaId, int officerid)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_secretary_approved == 1 && m.CreatedBy == officerid)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdChairmanApproved(int sabhaId, int officerid)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_chairman_approved == 1 && m.CreatedBy == officerid)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdMOHDisapproved(int sabhaId, int officerid)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_moh_approved == 2 && m.CreatedBy == officerid)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdSecretaryDisapproved(int sabhaId, int officerid)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_secretary_approved == 2 && m.CreatedBy == officerid)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdChairmanDisapproved(int sabhaId, int officerid)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_chairman_approved == 2 && m.CreatedBy == officerid)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }
        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdAllApproved(int sabhaId, int officerid)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.is_moh_approved == 1 && m.is_secretary_approved == 1 && m.is_chairman_approved == 1 && m.CreatedBy == officerid)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdDisapprovedAny(int sabhaId, int officerid)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && (m.is_moh_approved == 2 || m.is_secretary_approved == 2 || m.is_chairman_approved == 2) && m.CreatedBy == officerid)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdApprovalPendingAny(int sabhaId, int officerid)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && (m.is_moh_approved == 0 || m.is_secretary_approved == 0 || m.is_chairman_approved == 0) && m.CreatedBy == officerid)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdSabhaAndTaxStatus(int sabhaId, TaxStatus taxStatus, int officerid)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.Business.TaxTypeId == 3 && m.TaxState == taxStatus && m.CreatedBy == officerid)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessAndIndustrialTaxesForSabhaAndOfficerAndStatus(int sabhaId, TaxStatus taxStatus, int officerid)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && (m.Business.TaxTypeId == 1 || m.Business.TaxTypeId == 2) && m.TaxState == taxStatus && m.CreatedBy == officerid)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessAndIndustrialTaxesForSabhaAndStatus(int sabhaId, TaxStatus taxStatus)
        {
            return await controlDbContext.BusinessTaxes
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && (m.Business.TaxTypeId == 1 || m.Business.TaxTypeId == 2) && m.TaxState == taxStatus)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }
        //public async Task<BusinessTaxes> GetByApplicationNoAsync(string ApplicationNo)
        //{
        //    return await controlDbContext.BusinessTaxes
        //        .Where(m => m.Status == 1 && m.BusinessTaxes[0].ApplicationNo.Trim() == ApplicationNo.Trim()).FirstOrDefaultAsync();
        //}

        //public async Task<BusinessTaxes> GetByApplicationNoAndOfficeAsync(string ApplicationNo, int officeId)
        //{
        //    return await controlDbContext.BusinessTaxes
        //        .Where(m => m.Status == 1 && m.BusinessTaxes[0].ApplicationNo == ApplicationNo && m.OfficeId == officeId).FirstOrDefaultAsync();
        //}

        //public async Task<BusinessTaxes> GetByRegNoAsync(string RegNO)
        //{
        //    return await controlDbContext.BusinessTaxes
        //        .Where(m => m.Status == 1  && m.BusinessRegNo.Trim() == RegNO.Trim()).FirstOrDefaultAsync();
        //}

        //public async Task<IEnumerable<BusinessTaxes>> GetAllAsync(int Id)
        //{
        //    return await controlDbContext.BusinessTaxes
        //        .Where(m=> m.Status == 1)
        //        .ToListAsync();
        //}

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }

        //public async Task<IEnumerable<BusinessTaxes>> GetAllForBusinessSubNatureAndSabhaAsync(int SubNatureId, int sabhaId)
        //{
        //    return await controlDbContext.BusinessTaxes.Where(m =>  m.Status == 1 && m.SabhaId == sabhaId && m.BusinessSubNatureId == SubNatureId).ToListAsync();
        //}

        //public async Task<IEnumerable<BusinessTaxes>> GetAllForBusinessNatureAndSabhaAsync(int NatureId, int sabhaId)
        //{
        //    return await controlDbContext.BusinessTaxes.Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.BusinessNatureId == NatureId).ToListAsync();
        //}

        //public async Task<IEnumerable<BusinessTaxes>> GetAllForSabhaAsync(int sabhaId)
        //{
        //    return await controlDbContext.BusinessTaxes
        //        .Where(m => m.Status == 1 && m.SabhaId == sabhaId)
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<BusinessTaxes>> GetAllForBusinessOwnerIdAsync(int BusinessOwnerId)
        //{
        //    return await controlDbContext.BusinessTaxes
        //        .Where(m => m.Status == 1 && m.BusinessOwnerId == BusinessOwnerId)
        //        .ToListAsync();
        //}
    }
}