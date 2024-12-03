using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
        public interface IApplicationForConnectionService
        {
                Task<IEnumerable<ApplicationForConnection>> GetAllApplicationWaterConnections();


                Task<ApplicationForConnection> GetById(string applicationNo);

                Task<ApplicationForConnection> GetInfoById(string applicationNo);
                Task<ApplicationForConnection> GetApprovedApplicationInfoById(string applicationNo);

                Task<IEnumerable<ApplicationForConnection>> GetAllApplicationsForOffice(int officeId);
                Task<IEnumerable<ApplicationForConnection>> GetAllApplicationsForSabha(List<int> officeIdListOfSabha);
                Task<IEnumerable<ApplicationForConnection>> GetAllPendingApprovalApplicationsForSabha(List<int> officeIdListOfSabha);
                Task<IEnumerable<ApplicationForConnection>> GetAllPendingCustomerChangeRequest(List<int> officeIdListOfSabha);
                Task<IEnumerable<ApplicationForConnection>> GetAllNewlyApprovedApplicationsForSabha(List<int> officeIdListOfSabha);
                Task<IEnumerable<ApplicationForConnection>> GetAllNewlyApprovedApplicationsForOffice(int officeId);

                Task<ApplicationForConnection> Create(ApplicationForConnection obj, int OfficeId);

                Task<bool> Update(ApplicationForConnection objToBeUpdated, ApplicationForConnection obj);

                Task<IEnumerable<ApplicationForConnection>> ApproveWaterConnectionApplication(List<string> applicationIds, int approvedby);
                Task RejectWaterConnectionApplication(ApplicationForConnection objToBeUpdated, ApplicationForConnection obj);

                Task Delete(ApplicationForConnection obj);

                Task<ApplicationForConnection> IsDeleteble(string applicationNo);


        //--------------------------------------------------------------------------------------------------
        Task<IEnumerable<ApplicationForConnection>> GetAllRejectedApplicationsForOffice(int officeId);

                        Task<IEnumerable<ApplicationForConnection>> GetAllRejectedApplicationsForSabha(List<int> officeIdListOfSabha);
        
        //----- [Start - getAllApprovedApplicationsForOffice/{office id}] ---------------
        Task<IEnumerable<ApplicationForConnection>> GetAllApprovedApplicationsForOffice(int officeId);
        //----- [End - getAllApprovedApplicationsForOffice/{office id}] -----------------


        //----- [Start - getAllApprovedApplicationForSabha/{sabha Id}] -------------
        Task<IEnumerable<ApplicationForConnection>> GetAllApprovedApplicationsForSabha(List<int> officeIdListOfSabha);
        //----- [End - getAllApprovedApplicationForSabha/{sabha Id}] ---------------
                //--------------------------------------------------------------------------------------------------
        }
}
