using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmentBillAdjustmentService
    {
        Task<(bool, string?)> CreateBillAdjustmentRequest(AssessmentBillAdjustment assessmentBillAdjustment, HTokenClaim token);
        Task<(bool, string?)> ApproveRejectBillAdjustment(AssessmentBillAdjustment assessmentBillAdjustment, HTokenClaim token);
        Task<(int totalCount, IEnumerable<AssessmentBillAdjustment> list)> GetAllBillAdjustmentForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token);

        Task<(int totalCount, IEnumerable<AssessmentBillAdjustment> list)> GetAllPendingBillAdjustmentFor(int sabhaId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token);

        Task<(bool, string?)> WithdrawBillAdjustmentRequest(int assessmentBillAdjustmentId, HTokenClaim token);

    }
}