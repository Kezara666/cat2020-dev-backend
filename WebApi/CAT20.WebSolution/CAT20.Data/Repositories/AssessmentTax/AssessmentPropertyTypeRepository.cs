using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class AssessmentPropertyTypeRepository : Repository<AssessmentPropertyType>, IAssessmentPropertyTypeRepository
    {
        public AssessmentPropertyTypeRepository(DbContext context) : base(context)
        {
        }

        public async Task<AssessmentPropertyType> GetById(int id)
        {
            return await assessmentTaxDbContext.AssessmentPropertyTypes
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AssessmentPropertyType>> GetAll()
        {
            return
            await assessmentTaxDbContext.AssessmentPropertyTypes
                .ToListAsync();
        }

        public async Task<IEnumerable<AssessmentPropertyType>> GetAllForSabha(int sabhaId)
        {
            return
                await assessmentTaxDbContext.AssessmentPropertyTypes
                .Where(m => m.SabhaId == sabhaId)
                .ToListAsync();
        }

        public async Task<bool> IsRelationshipsExist(int id)
        {
            var p = await assessmentTaxDbContext.AssessmentPropertyTypes.Where(p => p.Id == id).Include(p => p.Assessments).FirstOrDefaultAsync();


            if (p!.Assessments!.Count() > 0)
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