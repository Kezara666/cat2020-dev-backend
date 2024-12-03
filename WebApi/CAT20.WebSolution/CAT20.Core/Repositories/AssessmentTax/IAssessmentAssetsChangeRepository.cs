using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAssessmentAssetsChangeRepository :IRepository<AssessmentAssetsChange>
    {
        Task<(int totalCount, IEnumerable<Assessment> list)> GetForPendingAssetsRequest(int? sabhaId, int pageNo);
        Task<(int totalCount, IEnumerable<AssessmentAssetsChange> list)> GetAllAssetsRequestForOffice(int? officeId, int pageNo);
        

        Task<bool> Create(AssessmentAssetsChange obj);
        Task<bool> ApproveRejectAssets(HApproveRejectJournal obj);

        Task<(int totalCount, IEnumerable<AssessmentAssetsChange> list)> GetAllAssetsChangeForAssessment(int assessmentId, int pageNo);
    }
}
