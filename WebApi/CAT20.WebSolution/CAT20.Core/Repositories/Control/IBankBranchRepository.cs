using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface IBankBranchRepository : IRepository<BankBranch>
    {
        Task<IEnumerable<BankBranch>> GetAllBankBranches();
        Task<BankBranch> GetBankBranchById(int id);
        Task<BankBranch> GetBankBranchByBankCodeAndBranchCode(int bankcode, int branchcode);
        Task<IEnumerable<BankBranch>> GetAllBankBranchesForBankCode(int bankcode);

        Task<BankBranch> GetBankBranchWithBankById(int branchId);
    }
}
