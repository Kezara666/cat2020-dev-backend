using CAT20.Core.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface IAssessmentFinalAccountService
    {
        Task<bool> UpdateVoteAssignForFinalAccounting(int sabhaId, HTokenClaim token);
        Task<(bool, string?)> AssessmentInti(int sabhaId ,HTokenClaim token);
        //Task<bool> AssessmentWarrant();
        //Task<bool> AssessmentYearEnd();
        //Task<bool> WaterInit();
        //Task<bool> WaterMonthly();
        //Task<bool> WaterYearEnd();

        Task<bool> FixMissedBalance(int sabhaId, HTokenClaim token);
    }
}
