using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class DescriptionRepository : Repository<Description>, IDescriptionRepository
    {
        public DescriptionRepository(DbContext context) : base(context)
        {
        }

        public async Task<Description> GetById(int id)
        {
            return await assessmentTaxDbContext.Descriptions
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Description>> GetAll()
        {
            return
            await assessmentTaxDbContext.Descriptions
                .ToListAsync();
        }

        public async Task<IEnumerable<Description>> GetAllForSabha(int sabhaId)
        {
            return
                await assessmentTaxDbContext.Descriptions
                .Where(m => m.SabhaId == sabhaId)
                .ToListAsync();
        }

        public async Task<bool> IsRelationshipsExist(int id)
        {
            var d = await assessmentTaxDbContext.Descriptions.Where(d => d.Id == id).Include(d => d.Assessments).FirstOrDefaultAsync();


            if (d!.Assessments!.Count() > 0)
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