using CAT20.Core.HelperModels;
using CAT20.Core.Models.HRM.LoanManagement;
using CAT20.Core.Models.HRM.PersonalFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.HRM.LoanManagement
{
    public interface IAdvanceBRepository : IRepository<AdvanceB>
    {
        Task<AdvanceB> GetLoanById(int id);
        Task<IEnumerable<AdvanceB>> GetAllLoans();
        Task<IEnumerable<AdvanceB>> GetAllLoansBySabha(int sabhaid);
        Task<IEnumerable<AdvanceB>> GetAllLoansByOffice(int officeid);
        Task<IEnumerable<AdvanceB>> GetAllLoansByEMPId(int empId);
        Task<IEnumerable<AdvanceB>> GetAllLoansByEMPIdAndLoanTypeId(int empId, int loantypeid);
        Task<IEnumerable<AdvanceB>> GetAllNewLoansBySabhaId(int sabhaid);

        Task<IEnumerable<AdvanceB>> GetAllAdvanceBForSettlementSabhaId(int sabhaid, HTokenClaim token);
    }

}
