using CAT20.Core.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Mixin
{
    public interface IBillAdjustmentService
    {
        Task<bool> AssessmentBillAdjustmentService(int adjustmentId, string actionNote, HTokenClaim token);
    }
}
