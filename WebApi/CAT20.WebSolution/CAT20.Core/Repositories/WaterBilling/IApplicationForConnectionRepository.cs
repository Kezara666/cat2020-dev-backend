using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.WaterBilling
{
    public interface IApplicationForConnectionRepository:IRepository<ApplicationForConnection>
    {
        Task<ApplicationForConnection> GetById(string applicationNo);
        Task<ApplicationForConnection> GetInfoById(string applicationNo);
        Task<ApplicationForConnection> GetApprovedApplicationInfoById(string applicationNo);
        Task<IEnumerable<ApplicationForConnection>> GetAllApplicationsForOffice(int officeId);
        Task<IEnumerable<ApplicationForConnection>> GetAllApplicationsForSabha(List<int> officeIdListOfSabha);
        Task<IEnumerable<ApplicationForConnection>> GetAllPendingApprovalApplicationsForSabha(List<int> officeIdListOfSabha);
        Task<IEnumerable<ApplicationForConnection>> GetAllNewlyApprovedApplicationsForSabha(List<int> officeIdListOfSabha);

        Task<IEnumerable<ApplicationForConnection>> GetAllNewlyApprovedApplicationsForOffice(int officeId);
        Task<IEnumerable<ApplicationForConnection>> GetAllByApplicationIds(List<string> applicationIds);
        Task<IEnumerable<ApplicationForConnection>> GetAllByApplicationInfoByOfficeId(int officeId);

        Task<ApplicationForConnection> IsDeleteble(string applicationNo);

        //--------------------------------------------------------------------------------------------------
        Task<IEnumerable<ApplicationForConnection>> GetAllRejectedApplicationsForOffice(int officeId);
        Task<IEnumerable<ApplicationForConnection>> GetAllRejectedApplicationsForSabha(List<int> officeIdListOfSabha);

        //----- [Start - getAllApprovedApplicationsForOffice/{office id}] ---------------
        Task<IEnumerable<ApplicationForConnection>> GetAllApprovedApplicationsForOffice(int officeId);
        //----- [End - getAllApprovedApplicationsForOffice/{office id}] -----------------


        //----- [Start - getAllApprovedApplicationForSabha/{sabha Id}] -------------
        Task<IEnumerable<ApplicationForConnection>> GetAllApprovedApplicationsForSabha(List<int> officeIdListOfSabha);
        Task<IEnumerable<ApplicationForConnection>> GetAllPendingCustomerChangeRequest(List<int> officeIdListOfSabha);
        //----- [End - getAllApprovedApplicationForSabha/{sabha Id}] ---------------
        //--------------------------------------------------------------------------------------------------
    }
}
