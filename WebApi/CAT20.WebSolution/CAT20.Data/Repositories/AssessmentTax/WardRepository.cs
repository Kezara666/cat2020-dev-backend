using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class WardRepository : Repository<Ward>, IWardRepository
    {
        public WardRepository(DbContext context) : base(context)
        {
        }

        public async Task<Ward> GetById(int id)
        {
            return await assessmentTaxDbContext.Wards
                .Include(w => w.Streets)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Ward>> GetAll()
        {
            return
            await assessmentTaxDbContext.Wards
            .Include(w => w.Streets)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ward>> GetAllForOffice(int officeId)
        {
            return
                await assessmentTaxDbContext.Wards.Include(w => w.Streets)
                .Where(m => m.OfficeId == officeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ward>> GetAllForSabha(int sabhaId)
        {
            var result =
                await assessmentTaxDbContext.Wards.Include(w => w.Streets)
                .Where(m => m.SabhaId == sabhaId)
                .ToListAsync();
            return result;
        }

        public async Task<bool> IsRelationshipsExist(int wardId)
        {
            var wd = await assessmentTaxDbContext.Wards.Where(w => w.Id == wardId).Include(w => w.Streets!).ThenInclude(st => st.Assessments).FirstOrDefaultAsync();


            if (wd!.Streets!.Count() > 0 || wd!.Streets!.Any(s => s.Assessments!.Count() > 0))
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