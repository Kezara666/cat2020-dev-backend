using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmentProcessService
    {

        Task<(int totalCount, IEnumerable<AssessmentProcess> list)> getAllProcessForSabha(int sabhaId, int pageNo);

        Task<bool> JanuaryEndProcess(AssessmentProcess assessmentProcess, object environment, string _backupFolder);
        Task<bool> Q1EndProcess(AssessmentProcess assessmentProcess, object environment, string _backupFolder);
        Task<bool> Q2EndProcess(AssessmentProcess assessmentProcess, object environment, string _backupFolder);
        Task<bool> Q3EndProcess(AssessmentProcess assessmentProcess, object environment, string _backupFolder);
        Task<bool> Q4EndProcess(AssessmentProcess assessmentProcess, object environment, string _backupFolder);
        Task<bool> YearEndProcess(AssessmentProcess assessmentProcess, object environment, string _backupFolder);


        //Task<bool> BackupProcess(int sabhaId, string processType);


    }
}
