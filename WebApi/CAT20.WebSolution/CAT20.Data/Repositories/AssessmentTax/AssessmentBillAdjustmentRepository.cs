using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class AssessmentBillAdjustmentRepository: Repository<AssessmentBillAdjustment>, IAssessmentBillAdjustmentRepository
    {
        public AssessmentBillAdjustmentRepository(DbContext context) : base(context)
        {
        }


        public Task<AssessmentBillAdjustment> GetById(int id, HTokenClaim token)
        {
           return assessmentTaxDbContext.AssessmentBillAdjustments
                .Include(a=>a.Assessment)
                .Where(m => m.Id == id && m.DraftApproveRejectWithdraw!=0 && m.Assessment.SabhaId == token.sabhaId)
                .FirstOrDefaultAsync();
        }

        public async Task<(int totalCount, IEnumerable<AssessmentBillAdjustment> list)> GetAllBillAdjustmentForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {

            if (filterKeyword != "undefined")
            {
                filterKeyword = "%" + filterKeyword + "%";
            }
            else if (filterKeyword == "undefined")
            {
                filterKeyword = null;
            }

            var keyword = filterKeyword ?? "";


            var result = assessmentTaxDbContext.AssessmentBillAdjustments
                 .Include(a => a.Assessment)
                  .Where(m => m.DraftApproveRejectWithdraw != 0 && m.Assessment.SabhaId == token.sabhaId)
                 .Where(a => EF.Functions.Like(a.Assessment.AssessmentNo!, "%" + filterKeyword + "%"))
                 .OrderBy(a => a.Id);




            int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);



        }

        public async Task<(int totalCount, IEnumerable<AssessmentBillAdjustment> list)> GetAllPendingBillAdjustmentFor(int sabhaId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            if (filterKeyword != "undefined")
            {
                filterKeyword = "%" + filterKeyword + "%";
            }
            else if (filterKeyword == "undefined")
            {
                filterKeyword = null;
            }

            var keyword = filterKeyword ?? "";



 

            var result = assessmentTaxDbContext.AssessmentBillAdjustments
                .Include(a => a.Assessment)
                .Where(m => m.Assessment!.SabhaId == sabhaId && m.Assessment.HasBillAdjustmentRequest == true && m.DraftApproveRejectWithdraw != 0)
                .Where(m =>
                        m.Assessment!.AssessmentStatus == AssessmentStatus.Active ||
                        m.Assessment!.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        m.Assessment!.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        m.Assessment!.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        m.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        m.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        m.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        m.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .Where(a => EF.Functions.Like(a.Assessment.AssessmentNo!, "%" + filterKeyword + "%"))
                .OrderBy(a => a.Id);


            int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }

        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }

    }

}
