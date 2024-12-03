using CAT20.Core.Models.Control;
using CAT20.Core.Repositories.Control;
using log4net;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.Control
{
    public class PartnerRepository : Repository<Partner>, IPartnerRepository
    {
        public PartnerRepository(DbContext context) : base(context)
        {
        }

        public async Task<Partner> GetByIdAsync(int id)
        {
            return await controlDbContext.Partner.Include(m=>m.PartnerMobiles)
                 .Include(p => p.GnDivision)
                 .Include(m => m.PartnerMobiles)
                 .Include(m => m.PermittedThirdPartyAssessments)
                .Where(m => m.Active == 1 && m.Id == id)
                .FirstOrDefaultAsync();
        }

        public Partner GetByIdSynchronously(int id)
        {
            return  controlDbContext.Partner
                //.Include(m => m.PartnerMobiles)
                 //.Include(p => p.GnDivision)
                 //.Include(m => m.PartnerMobiles)
                 //.Include(m => m.PermittedThirdPartyAssessments)
                .Where(m => m.Active == 1 && m.Id == id)
                .FirstOrDefault();
        }

        public async Task<Partner> GetByNICAsync(string NIC)
        {
            return await controlDbContext.Partner
                 .Include(p => p.GnDivision)
                 .Include(m => m.PartnerMobiles)
                 .Include(m => m.PermittedThirdPartyAssessments)
                .Where(m => m.IsTempory == 0 && m.Active == 1 && m.NicNumber.Trim() == NIC.Trim())
                .FirstOrDefaultAsync();
        }
        public async Task<Partner> GetByPassportNoAsync(string passport)
        {
            return await controlDbContext.Partner
                 .Include(p => p.GnDivision)
                 .Include(m => m.PartnerMobiles)
                 .Include(m => m.PermittedThirdPartyAssessments)
                .Where(m => m.IsTempory == 0 && m.Active == 1 && m.PassportNo.Trim() == passport.Trim())
                .FirstOrDefaultAsync();
        }

        public async Task<Partner> GetByPhoneNoAsync(string PhoneNo)
        {
            return await controlDbContext.Partner
                .Include(p => p.GnDivision)
                .Include(m => m.PartnerMobiles)
                 .Include(m => m.PermittedThirdPartyAssessments)
                .Where(m => m.IsTempory == 0 && m.Active == 1 && m.MobileNumber.Trim() == PhoneNo.Trim())
                .FirstOrDefaultAsync();
        }

        public async Task<Partner> GetByEmailAsync(string email)
        {
            return await controlDbContext.Partner
                .Include(p => p.GnDivision)
                .Include(m => m.PartnerMobiles)
                 .Include(m => m.PermittedThirdPartyAssessments)
                .Where(m => m.IsTempory == 0 && m.Active == 1 && m.Email.Trim() == email.Trim())
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Partner>> GetAllAsync()
        {
            return await controlDbContext.Partner
                .Where(m => m.Active == 1 && m.IsTempory == 0)
                .ToListAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }

        public async Task<IEnumerable<Partner>> GetAllForPartnerTypeAsync(int Id)
        {
            return await controlDbContext.Partner.Where(m => m.IsEditable == Id && m.Active == 1 && m.IsTempory == 0 && m.IsBusiness == 0).ToListAsync();
        }

        public async Task<IEnumerable<Partner>> GetAllForPartnerTypeAndSabhaAsync(int Id, int sabhaId)
        {
            return await controlDbContext.Partner.Where(m => m.IsEditable == Id && m.Active == 1 && m.SabhaId == sabhaId && m.IsTempory == 0 && m.IsBusiness == 0).ToListAsync();
        }

        public async Task<IEnumerable<Partner>> GetAllForSabhaAsync(int sabhaId)
        {
            return await controlDbContext.Partner
                .Include(p=>p.GnDivision)
                .Where(m => m.Active == 1 && m.SabhaId == sabhaId && m.IsTempory == 0 && m.IsBusiness == 0)
                .OrderByDescending(m=>m.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Partner>> GetAllCustomersForSabhaAsync(int sabhaId)
        {
            return await controlDbContext.Partner
                .Include(p => p.GnDivision)
                .Where(m => m.Active == 1 && m.SabhaId == sabhaId && m.IsTempory == 0 && m.IsBusiness==0)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Partner>> GetAllBusinessesForSabhaAsync(int sabhaId)
        {
            return await controlDbContext.Partner
                .Include(p => p.GnDivision)
                .Where(m => m.Active == 1 && m.SabhaId == sabhaId && m.IsTempory == 0 && m.IsBusiness == 1)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Partner>> GetAllBusinessOwnersForSabhaAsync(int sabhaId)
        {
            var businessOwners = await controlDbContext.Partner
            .Where(m => controlDbContext.Businesses
            .Select(b => b.BusinessOwnerId)
            .Contains(m.Id)
                && m.SabhaId == sabhaId && m.IsTempory == 0 && m.Active == 1)
            .OrderByDescending(m => m.Id)
            .ToListAsync();

            return businessOwners;
        }

        public async Task<IEnumerable<Partner>> GetAllBusinessOwnersForOfficeAsync(int sabhaId)
        {
            //return await controlDbContext.Partner
            //    .Where(m => m.Active == 1 && m.SabhaId == sabhaId && m.IsTempory == 0 && m.IsBusinessOwner == 1)
            //    .ToListAsync();
            return null;
        }

        public async Task<IEnumerable<Partner>> GetAllForPartnersByIds(List<int?> ids)
        {
            return await controlDbContext.Partner.Where(pt => ids.Contains(pt.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Partner>> GetAllForPartnersByIdsThenSearchNic(List<int?> ids, string? query)
        {
            return await controlDbContext.Partner
                .Where(pt => ids.Contains(pt.Id))
                .Where(pt => EF.Functions.Like(pt.NicNumber!, "%" + query + "%"))
                .Take(200)
                .ToListAsync();
        }

        public async Task<IEnumerable<Partner>> GetAllForPartnersByIdsThenSearchName(List<int?> ids, string? query)
        {
            return await controlDbContext.Partner
                .Where(pt => ids.Contains(pt.Id))
                .Where(pt => EF.Functions.Like(pt.Name!, "%" + query + "%"))
                .Take(200)
                .ToListAsync();
        }

        public async Task<Partner> GetByIdWithDetails(int id)
        {
            return await controlDbContext.Partner
                .Include(p => p.GnDivision)
                .Include(m => m.PartnerMobiles)
                 .Include(m => m.PermittedThirdPartyAssessments)
                .Where(pt => pt.Id == id).FirstOrDefaultAsync();
        }

        public async Task<(int totalCount, IEnumerable<Partner> list)> GetAllForPartnersBySearchQuery(int? sabhaId, List<int?> includedIds, string? query, int pageNo, int pageSize)
        {
            var results = controlDbContext.Partner
            .Where(pt => sabhaId.HasValue ? pt.SabhaId == sabhaId : true)
            .Where(pt =>  includedIds.Contains(pt.Id) || EF.Functions.Like(pt.NicNumber!, "%" + query + "%") || EF.Functions.Like(pt.PhoneNumber!, "%" + query + "%") || EF.Functions.Like(pt.Name, "%" + query + "%"))
            .OrderBy(pt => pt.Id);

            int totalCount = await results.CountAsync();

            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var partners = await results.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, partners);
        }


        public async Task<bool> IsBusinessRegNoExist(string businessRegNo)
        {
            return await controlDbContext.Partner
                 .AnyAsync(m => m.Active == 1 && m.BusinessRegNo == businessRegNo.Trim() && m.IsBusiness == 1);
               
        }

        public async Task<Partner> GetBusinessByRegNo(string regNo)
        {
            return  await controlDbContext.Partner
                  //.Include(p => p.GnDivision)
                  .Include(m => m.PartnerMobiles)
                  .Include(m => m.PermittedThirdPartyAssessments)
                 .Where(m => m.IsTempory == 0 && m.Active == 1 && m.BusinessRegNo == regNo.Trim() && m.IsBusiness==1)
                 .FirstOrDefaultAsync();
        }

        public async Task<Partner> GetBusinessByPhoneNo(string phoneNo)
        {
            return await controlDbContext.Partner
               //.Include(p => p.GnDivision)
               .Include(m => m.PartnerMobiles)
                .Include(m => m.PermittedThirdPartyAssessments)
               .Where(m => m.IsTempory == 0 && m.Active == 1 && m.IsBusiness ==1 && m.MobileNumber.Trim() == phoneNo.Trim())
               .FirstOrDefaultAsync();
        }

        public async Task<Partner> GetWithDocumentsByIdAsync(int id)
        {
            return await controlDbContext.Partner
                .Include(m => m.PartnerMobiles)
                .Include(p => p.GnDivision)
                .Include(m => m.PermittedThirdPartyAssessments)
                .Include(m => m.PartnerDocuments.Where(pd => pd.Status == true))
                .Where(m => m.Active == 1 && m.Id == id)
                .FirstOrDefaultAsync();
        }

    }
}