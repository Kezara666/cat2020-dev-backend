using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Control
{
    public class BusinessRepository : Repository<Business>, IBusinessRepository
    {
        public BusinessRepository(DbContext context) : base(context)
        {
        }
        public async Task<Business> GetByIdAsync(int id)
        {
            return await controlDbContext.Businesses
                //.Include(b=> b.BusinessTaxes)
                .Where(m => m.Id == id && m.Status == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<Business> GetByRegNoAndOfficeAsync(string RegNo, int officeId)
        {
            return await controlDbContext.Businesses
                //.Include(b => b.BusinessTaxes)
                .Where(m => m.Status == 1 && m.BusinessRegNo.Trim() == RegNo && m.OfficeId == officeId).FirstOrDefaultAsync();
        }

        public async Task<Business> GetByApplicationNoAsync(string ApplicationNo)
        {
            return await controlDbContext.Businesses
                //.Include(b => b.BusinessTaxes)
                .Where(m => m.Status == 1 && m.BusinessTaxes[0].ApplicationNo.Trim() == ApplicationNo.Trim()).FirstOrDefaultAsync();
        }

        public async Task<Business> GetByApplicationNoAndOfficeAsync(string ApplicationNo, int officeId)
        {
            return await controlDbContext.Businesses
                //.Include(b => b.BusinessTaxes)
                .Where(m => m.Status == 1 && m.BusinessTaxes[0].ApplicationNo == ApplicationNo && m.OfficeId == officeId).FirstOrDefaultAsync();
        }

        public async Task<Business> GetByRegNoAsync(string RegNO)
        {
            return await controlDbContext.Businesses
                //.Include(b => b.BusinessTaxes)
                .Where(m => m.Status == 1  && m.BusinessRegNo.Trim() == RegNO.Trim()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Business>> GetAllAsync(int Id)
        {
            return await controlDbContext.Businesses
                //.Include(b => b.BusinessTaxes)
                .Where(m=> m.Status == 1)
                .ToListAsync();
        }
        
        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }

        public async Task<IEnumerable<Business>> GetAllForBusinessSubNatureAndSabhaAsync(int SubNatureId, int sabhaId)
        {
            return await controlDbContext.Businesses
                //.Include(b => b.BusinessTaxes)
                .Where(m =>  m.Status == 1 && m.SabhaId == sabhaId && m.BusinessSubNatureId == SubNatureId).ToListAsync();
        }

        public async Task<IEnumerable<Business>> GetAllForBusinessNatureAndSabhaAsync(int NatureId, int sabhaId)
        {
            return await controlDbContext.Businesses
                //.Include(b => b.BusinessTaxes)
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.BusinessNatureId == NatureId).ToListAsync();
        }

        public async Task<IEnumerable<Business>> GetAllForSabhaAsync(int sabhaId)
        {
            return await controlDbContext.Businesses
                //.Include(b => b.BusinessTaxes)
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Business>> GetAllForBusinessOwnerIdAsync(int BusinessOwnerId)
        {
            return await controlDbContext.Businesses
                //.Include(b => b.BusinessTaxes)
                .Where(m => m.Status == 1 && m.BusinessOwnerId == BusinessOwnerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Business>> GetAllBusinessAndIndustrialTaxesForOwnerId(int businessownerid)
        {
            return await controlDbContext.Businesses
                //.Include(b => b.BusinessTaxes)
                .Where(m => m.Status == 1 && m.BusinessOwnerId == businessownerid && (m.TaxTypeId == 1 || m.TaxTypeId == 2))
                .ToListAsync();
        }

        public async Task<IEnumerable<Business>> GetAllBusinessAndIndustrialTaxesForOwnerIdAndSabhaId(int businessownerid, int sabhaid)
        {
            return await controlDbContext.Businesses
                //.Include(b => b.BusinessTaxes)
                .Where(m => m.Status == 1 && m.BusinessOwnerId == businessownerid  && m.SabhaId==sabhaid && (m.TaxTypeId == 1 || m.TaxTypeId == 2))
                .ToListAsync();
        }

        public async Task<IEnumerable<Business>> GetAllBusinessAndIndustrialTaxesForSabha(int sabhaId)
        {
            return await controlDbContext.Businesses
                //.Include(b => b.BusinessTaxes)
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && (m.TaxTypeId==1 || m.TaxTypeId == 2))
                .ToListAsync();
        }

        public async Task<IEnumerable<Business>> GetAllBusinessLicensesForSabha(int sabhaId)
        {
            return await controlDbContext.Businesses
                //.Include(b => b.BusinessTaxes)
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.TaxTypeId == 3)
                .ToListAsync();
        }

        public async Task<IEnumerable<Business>> GetAllBusinessAndIndustrialTaxesForSabhaAndOfficer(int sabhaId, int officerid)
        {
            return await controlDbContext.Businesses
                //.Include(b => b.BusinessTaxes)
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && (m.TaxTypeId == 1 || m.TaxTypeId == 2) && m.CreatedBy == officerid)
                .ToListAsync();
        }

        public async Task<IEnumerable<Business>> GetAllBusinessLicensesForSabhaAndOfficer(int sabhaId, int officerid)
        {
            return await controlDbContext.Businesses
                //.Include(b => b.BusinessTaxes)
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.TaxTypeId == 3 && m.CreatedBy==officerid)
                .ToListAsync();
        }
    }
}