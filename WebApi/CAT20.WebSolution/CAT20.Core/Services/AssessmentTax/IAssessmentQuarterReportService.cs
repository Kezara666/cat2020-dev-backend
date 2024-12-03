using CAT20.Core.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmentQuarterReportService
    {
        public Task<(bool, List<int>, string)> CreateInitialReport(List<int> includeIds);

        public Task<(bool, List<int>,string)> CreateReportQ1(List<int> includeIds);
        public Task<(bool, List<int>,string)> CreateReportQ2(List<int> includeIds);
        public Task<(bool, List<int>,string)> CreateReportQ3(List<int> includeIds);
        public Task<(bool, List<int>,string)> CreateReportQ4(List<int> includeIds);

        public Task <bool> UpdateAnnulAmount();
    }
}
