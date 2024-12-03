using CAT20.Core.DTO.HRM;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.HRM.LoanManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.HRM.LoanManagement
{
    public interface IAdvanceBTypeDataService
    {
        Task<AdvanceBTypeData> GetAdvanceBTypeDataById(int id);
      
        Task<IEnumerable<AdvanceBTypeData>> GetAdvanceBTypeDataByAccountSystemVersionAndSabhaAsync(int accountsystemversionid, int sabhaid);

        Task<IEnumerable<AdvanceBTypeData>> GetAllLoanTypeData();
        Task<(bool, string?)> saveAdvancedBLoanVoteAssignment(SaveAdvancedBLoanLedgerTypeMapping saveAdvancedBLoanLedgerTypeMappingResource, HTokenClaim token);

        Task<IEnumerable<AdvanceBTypeLedgerMapping>> GetAllAdvancedLedgerTypesMappingForSabha(int sabhaId);
    }
}
