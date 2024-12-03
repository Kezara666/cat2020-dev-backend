using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class AssessmentTempSubPartnerRepository : Repository<Core.Models.AssessmentTax.AssessmentTempSubPartner>, Core.Repositories.AssessmentTax.IAssessmentTempSubPartnerRepository
    {
        public AssessmentTempSubPartnerRepository(DbContext context) : base(context)
        {
        }

        public async Task<Core.Models.AssessmentTax.AssessmentTempSubPartner> GetById(int id)
        {
            //return await assessmentTaxDbContext.SubOwners
            //    .Include(m => m.Assessment)
            //    .Where(p => p.Id == id)
            //    .FirstOrDefaultAsync();
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Core.Models.AssessmentTax.AssessmentTempSubPartner>> GetAll()
        {
            //return
            //await assessmentTaxDbContext.SubOwners
            //.Include(m => m.Assessment)
            //    .ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Core.Models.AssessmentTax.AssessmentTempSubPartner>> GetAllForOffice(int officeId)
        {
            //return
            //    await assessmentTaxDbContext.SubOwners
            //    .Include(m => m.Assessment)
            //    .Where(m => m.Assessment.OfficeId == officeId)
            //    .ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Core.Models.AssessmentTax.AssessmentTempSubPartner>> GetAllForSabha(int sabhaId)
        {
            //return
            //    await assessmentTaxDbContext.SubOwners
            //    .Include(m => m.Assessment)
            //    .Where(m => m.Assessment.SabhaId == sabhaId)
            //    .ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Core.Models.AssessmentTax.AssessmentTempSubPartner>> GetAllForAssessmentId(int assessmentId)
        {
            //return
            //    await assessmentTaxDbContext.SubOwners
            //    .Include(m => m.Assessment)
            //    .Where(m => m.AssessmentId == assessmentId)
            //    .ToListAsync();
            throw new NotImplementedException();
        }

        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }

    }
}