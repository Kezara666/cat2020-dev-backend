using CAT20.Core.Models.HRM.LoanManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.HRM.LoanManagement
{
    public interface IAdvanceBTypeDataRepository : IRepository<AdvanceBTypeData>
    {
        Task<AdvanceBTypeData> GetLoanTypeDataById(int id);
        Task<IEnumerable<AdvanceBTypeData>> GetLoanTypeData();
        Task<IEnumerable<AdvanceBTypeData>> GetLoanTypeDataByAccountSystemVersionAndSabhaAsync(int accountsystemversionid, int sabhaid);

        Task<IEnumerable<AdvanceBTypeLedgerMapping>> GetAllAdvancedLedgerTypesMappingForSabha(int sabhaId);
    }

}
