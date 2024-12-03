using CAT20.Core.DTO.Assessment;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmentRenewalService
    {
        Task<(bool, string, byte[])> GetExcelForRenewal(int sabhaId, HTokenClaim token);
        Task<(bool, string, byte[])> GetExcelForNew(int sabhaId, HTokenClaim token);
        Task<(bool, string)> CreateAssessmentRenewal(List<AssessmentRenewal> assessmentRenewals, HTokenClaim token);
        Task<bool> UpdateAssessmentNextYearQuarters(HTokenClaim token, int? propertyTypeId);
    }
}
