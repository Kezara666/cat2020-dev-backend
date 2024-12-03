using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAssessmentBillAdjustmentRepository : IRepository<AssessmentBillAdjustment>
    {

        Task<AssessmentBillAdjustment> GetById(int id, HTokenClaim token);

        Task<(int totalCount, IEnumerable<AssessmentBillAdjustment> list)> GetAllBillAdjustmentForSabha(int SabhaId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token);

        Task<(int totalCount, IEnumerable<AssessmentBillAdjustment> list)> GetAllPendingBillAdjustmentFor(int sabhaIs, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token);
    }
}
