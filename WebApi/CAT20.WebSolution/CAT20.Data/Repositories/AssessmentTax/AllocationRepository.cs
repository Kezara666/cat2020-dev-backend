using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class AllocationRepository : Repository<Allocation>, IAllocationRepository
    {
        public AllocationRepository(DbContext context) : base(context)
        {
        }

        public async Task<Allocation> GetById(int id)
        {
            return await assessmentTaxDbContext.Allocations
                .Include(m => m.Assessment)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Allocation> GetForAssessment(int assessmentId)
        {
            //return
            //   await assessmentTaxDbContext.Allocations
            //   .Include(m => m.Assessment)
            //   .Where(m => m.AssessmentId == assessmentId)
            //   .FirstOrDefaultAsync();


            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Allocation>> GetAll()
        {
            return
            await assessmentTaxDbContext.Allocations
                .ToListAsync();
        }

        public async Task<IEnumerable<Allocation>> GetAllForOffice(int officeId)
        {
            return
                await assessmentTaxDbContext.Allocations
                .Include(m => m.Assessment)
                .Where(m => m.Assessment.OfficeId == officeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Allocation>> GetAllForSabha(int sabhaId)
        {
            return
               await assessmentTaxDbContext.Allocations
               .Include(m => m.Assessment)
               .Where(m => m.Assessment.SabhaId == sabhaId)
               .ToListAsync();
        }

        public async Task<IEnumerable<Allocation>> GetForNextYearAllocationsUpdate(int sabhaId)
        {
            return
              await assessmentTaxDbContext.Allocations
              .Where(m => m.Assessment!.SabhaId == sabhaId)
               .Where(a =>
                        a.Assessment!.AssessmentStatus == AssessmentStatus.Active ||
                        a.Assessment!.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.Assessment!.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.Assessment!.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
              .ToListAsync();
        }

        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }

    }
}