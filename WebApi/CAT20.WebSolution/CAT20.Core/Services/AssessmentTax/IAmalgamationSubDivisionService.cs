using CAT20.Core.DTO.Assessment.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAmalgamationSubDivisionService
    {
        Task<(bool, string?)> CreateAmalgamation(SaveAssessmentAmalgamationResource saveAssessmentAmalgamationResources, HTokenClaim token);
        Task<(bool, string?)> CreateSubDivisions(SaveAssessmentSubDivisionResource saveAssessmentSubDivisionResource, HTokenClaim token);
        Task<(bool, string?)> ApproveRejectAmalgamationSubdivisions(SaveAmalgamationSubDivisionActionsResource actionsResource, HTokenClaim token);

        Task<(int totalCount, IEnumerable<AmalgamationSubDivision> list)> GetAllAmalgamationSubDivisionForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token);
        Task<(int totalCount, IEnumerable<AmalgamationSubDivision> list)> GetAllPendingAmalgamationSubDivisionForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token);
    }
}
