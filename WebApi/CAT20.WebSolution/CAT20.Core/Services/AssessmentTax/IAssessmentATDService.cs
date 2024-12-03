using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmentATDService
    {
        Task<AssessmentATD> GetATDById(int Id);
        Task<(int totalCount, IEnumerable<AssessmentATD> list)> GetAllATDRequestsForSabha(int? sabhaId, ATDRequestStatus atdStatus, int pageNo);
        Task<(int totalCount, IEnumerable<AssessmentATD> list)> GetAllATDRequestsForOffice(int? officeId, ATDRequestStatus atdStatus, int pageNo);
        Task<(int totalCount, IEnumerable<AssessmentATD> list)> GetAllATDRequestsForAssessment(int assessmentId, int pageNo);

        Task<bool> Save(AssessmentATD obj, HTokenClaim token);
        Task<bool> ApproveRejectATD(HApproveRejectATD obj);
        Task Delete(AssessmentATD obj);
    }
}
