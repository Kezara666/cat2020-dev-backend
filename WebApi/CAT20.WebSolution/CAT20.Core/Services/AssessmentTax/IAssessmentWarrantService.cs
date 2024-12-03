using CAT20.Core.HelperModels;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmentWarrantService
    {

        /*****
         * 
         * Following use for method one   For Quarter amount
         * 
         * 
         */
        Task<bool> Q1Warranting(HAssessmentWarrant assessmentWarrant, HTokenClaim token);
        Task<bool> Q2Warranting(HAssessmentWarrant assessmentWarrant, HTokenClaim token);
        Task<bool> Q3Warranting(HAssessmentWarrant assessmentWarrant, HTokenClaim token);
        Task<bool> Q4Warranting(HAssessmentWarrant assessmentWarrant , HTokenClaim token);

        /*****
         * 
         * Following use for method Two  For Remaining Balance 
         *  
         * 
         */

        Task<bool> Q1WarrantingMethod2(HAssessmentWarrant assessmentWarrant , HTokenClaim token);
        Task<bool> Q2WarrantingMethod2(HAssessmentWarrant assessmentWarrant , HTokenClaim token);
        Task<bool> Q3WarrantingMethod2(HAssessmentWarrant assessmentWarrant , HTokenClaim token);
        Task<bool> Q4WarrantingMethod2(HAssessmentWarrant assessmentWarrant , HTokenClaim token);


    }
}
