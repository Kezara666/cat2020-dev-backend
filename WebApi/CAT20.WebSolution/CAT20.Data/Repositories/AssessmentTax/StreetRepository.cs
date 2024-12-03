using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class StreetRepository : Repository<Street>, IStreetRepository
    {
        public StreetRepository(DbContext context) : base(context)
        {
        }

        public async Task<Street> GetById(int id)
        {
            return await assessmentTaxDbContext.Streets
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Street>> GetAll()
        {
            return
            await assessmentTaxDbContext.Streets
                .ToListAsync();
        }

        public async Task<IEnumerable<Street>> GetAllForOffice(int officeId)
        {
            return
                await assessmentTaxDbContext.Streets
                .Include(wd => wd.Ward)
                .Where(m => m.Ward.OfficeId == officeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Street>> GetAllForSabha(int sabhaId)
        {
            return
                await assessmentTaxDbContext.Streets
                .AsNoTracking()
                .Include(wd => wd.Ward)
                .Where(m => m.Ward.SabhaId == sabhaId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Street>> GetAllForWard(int wardid)
        {
            return
                await assessmentTaxDbContext.Streets
                .Where(m => m.WardId == wardid)
                .ToListAsync();
        }

        public async Task<bool> IsRelationshipsExist(int streetId)
        {
            var st = await assessmentTaxDbContext.Streets.Where(st => st.Id == streetId).Include(st => st.Assessments).FirstOrDefaultAsync();


            if (st!.Assessments!.Count() > 0)
            {
                return false;
            }


            return true;

        }

        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }

    }
}