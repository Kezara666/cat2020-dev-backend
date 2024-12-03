using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.DTO.HRM;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.HRM.LoanManagement;

namespace CAT20.Core.Repositories.HRM
{
    public interface IAdvancedBTypeLedgerMappingRepository : IRepository<AdvanceBTypeLedgerMapping>
    {
       // Task<(bool, string?)> saveAdvancedBLoanVoteAssignment(SaveAdvancedBLoanLedgerTypeMapping saveAdvancedBLoanLedgerTypeMappingResource, HTokenClaim token);
    }
}
