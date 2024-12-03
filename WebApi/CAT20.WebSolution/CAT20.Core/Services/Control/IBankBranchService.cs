using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IBankBranchService
    {
        Task<IEnumerable<BankBranch>> GetAllBankBranches();
        Task<BankBranch> GetBankBranchById(int id);
        Task<BankBranch> GetBankBranchByBankCodeAndBranchCode(int bankcode, int branchcode);
        Task<IEnumerable<BankBranch>> GetAllBankBranchesForBankCode(int bankcode);

        Task<BankBranch> GetBankBranchWithBankById(int branchId);
    }
}

