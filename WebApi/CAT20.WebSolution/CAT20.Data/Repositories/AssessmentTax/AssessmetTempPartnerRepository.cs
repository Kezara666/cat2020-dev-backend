using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class AssessmetTempPartnerRepository : Repository<AssessmentTempPartner>, IAssessmetTempPartnerRepository
    {
        public AssessmetTempPartnerRepository(DbContext context) : base(context)
        {
        }

        public async Task<AssessmentTempPartner> GetById(int id)
        {
            return await assessmentTaxDbContext.AssessmentTempPartners
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        //public async Task<IEnumerable<AssessmetTempPartner>> GetAll()
        //{
        //    return
        //    await assessmentTaxDbContext.AssessmetTempPartners
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<AssessmetTempPartner>> GetAllForOffice(int officeId)
        //{
        //    return
        //        await assessmentTaxDbContext.AssessmetTempPartners
        //        .Include(wd => wd.assessments)
        //        .Where(m => m.Ward.OfficeId == officeId)
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<AssessmetTempPartner>> GetAllForSabha(int sabhaId)
        //{
        //    return
        //        await assessmentTaxDbContext.AssessmetTempPartners
        //        .Include(wd=> wd.Ward)
        //        .Where(m => m.Ward.SabhaId == sabhaId)
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<AssessmetTempPartner>> GetAllForWard(int wardid)
        //{
        //    return
        //        await assessmentTaxDbContext.AssessmetTempPartners
        //        .Where(m => m.WardId == wardid)
        //        .ToListAsync();
        //}

        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }

    }
}