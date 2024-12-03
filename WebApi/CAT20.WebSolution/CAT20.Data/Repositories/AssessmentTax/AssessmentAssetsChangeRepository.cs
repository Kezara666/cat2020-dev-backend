using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class AssessmentAssetsChangeRepository : Repository<AssessmentAssetsChange>, IAssessmentAssetsChangeRepository
    {
        public AssessmentAssetsChangeRepository(DbContext context) : base(context)
        {
        }

        public Task<bool> ApproveRejectAssets(HApproveRejectJournal obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(AssessmentAssetsChange obj)
        {
            throw new NotImplementedException();
        }

        public async Task<(int totalCount, IEnumerable<AssessmentAssetsChange> list)> GetAllAssetsChangeForAssessment(int assessmentId, int pageNo)
        {
            try
            {

                var query = assessmentTaxDbContext.AssessmentAssetsChanges
                    .Where(a => a.AssessmentId == assessmentId)
                    .OrderByDescending(a => a.Id); ;

                // Implement your logic to calculate totalCount

                int totalCount = await query.CountAsync();

                var pageSize = 10;
                int skipAmount = (pageNo - 1) * pageSize;

                var list = await query.Skip(skipAmount).Take(pageSize).ToListAsync();

                return (totalCount: totalCount, list: list);

            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // You might want to return an appropriate error response.
                return (totalCount: 0, list: null);
            }
        }

        public async Task<(int totalCount, IEnumerable<AssessmentAssetsChange> list)> GetAllAssetsRequestForOffice(int? officeId, int pageNo)
        {
            try
            {

                var query = assessmentTaxDbContext.AssessmentAssetsChanges
                    .AsNoTracking()
                    .Include(j => j.Assessment)
                    .Where(a => a.Assessment!.OfficeId == officeId)
                    .OrderByDescending(a => a.Id); ;

                // Implement your logic to calculate totalCount

                int totalCount = await query.CountAsync();

                var pageSize = 10;
                int skipAmount = (pageNo - 1) * pageSize;

                var list = await query.Skip(skipAmount).Take(pageSize).ToListAsync();

                return (totalCount: totalCount, list: list);

            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // You might want to return an appropriate error response.
                return (totalCount: 0, list: null);
            }
        }


        public async Task<(int totalCount, IEnumerable<Assessment> list)> GetForPendingAssetsRequest(int? sabhaId, int pageNo)
        {
            try
            {

                var query = assessmentTaxDbContext.Assessments
               .AsNoTracking()
               .Include(m => m.Street).ThenInclude(st => st.Ward)
               .Include(m => m.AssessmentPropertyType)
               .Include(m => m.Description)
               .Include(m => m.AssessmentTempPartner)
               .Include(m => m.AssessmentTempSubPartner)
               .Include(m => m.AssessmentBalance)
               .Include(a => a.AssessmentBalance.Q1)
               .Include(a => a.AssessmentBalance.Q2)
               .Include(a => a.AssessmentBalance.Q3)
               .Include(a => a.AssessmentBalance.Q4)
               .Include(m => m.Allocation)
               .Include(a => a.AssessmentAssetsChange!.OrderByDescending(t => t.Id))
               .Where(a => a.SabhaId == sabhaId && a.HasAssetsChangeRequest == true);



                // Implement your logic to calculate totalCount

                int totalCount = await query.CountAsync();

                var pageSize = 10;
                int skipAmount = (pageNo - 1) * pageSize;

                var assessmentList = await query.Skip(skipAmount).Take(pageSize).ToListAsync();

                return (totalCount: totalCount, list: assessmentList);

            }
            catch (Exception ex)
            {

                return (totalCount: 0, list: null);
            }
        }

        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }
    }
}
