using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAssessmentATDRepository :IRepository<AssessmentATD>
    {
        Task<AssessmentATD> GetATDById(int Id);
        Task<(int totalCount, IEnumerable<AssessmentATD> list)> GetAllATDRequestsForSabha(int? sabhaId, ATDRequestStatus atdStatus, int pageNo);
        Task<(int totalCount, IEnumerable<AssessmentATD> list)> GetAllATDRequestsForOffice(int? officeId, ATDRequestStatus atdStatus, int pageNo);
        //Task<(int totalCount, IEnumerable<AssessmentATD> list)> GetAllATDRequestsByRequestStatus(ATDRequestStatus requestStatus, int pageNo);
        Task<(int totalCount, IEnumerable<AssessmentATD> list)> GetAllATDRequestsForAssessment(int assessmentId, int pageNo);
    }
}
