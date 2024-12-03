using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAssessmentQuarterReportRepository : IRepository<AssessmentQuarterReport>
    {
         
        
        Task<IEnumerable<AssessmentQuarterReport>> GetAllByAssessmentId(int assessmentId);

        Task<bool> HasReportExist(int assessmentId, int year, int qno);
        Task<IEnumerable<Assessment>> GetForInitialReport(int sabhaId);
         Task<IEnumerable<Assessment>> GetForQ1ReportForSabha(int sabhaId);
         Task<IEnumerable<Assessment>> GetForQ2ReportForSabha(int sabhaId);
         Task<IEnumerable<Assessment>> GetForQ3ReportForSabha(int sabhaId);
         Task<IEnumerable<Assessment>> GetForQ4ReportForSabha(int sabhaId);
         Task<IEnumerable<AssessmentProcess>> GetAllSbahaFinishedQEND(List<int> includeIds, AssessmentProcessType processType);

        Task<IEnumerable<AssessmentQuarterReport>> GetForAnnualAmount();
    }
}
