using CAT20.Core.HelperModels;
using CAT20.Core.Models.HRM.LoanManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.HRM.LoanManagement
{
    public interface IAdvanceBService
    {
        Task<AdvanceB> GetLoanById(int id);
        Task<IEnumerable<AdvanceB>> GetAllLoans();
        Task<IEnumerable<AdvanceB>> GetAllLoansBySabha(int sabhaid);
        Task<IEnumerable<AdvanceB>> GetAllLoansByOffice(int officeid);
        Task<IEnumerable<AdvanceB>> GetAllLoansByEMPId(int empId);
        Task<IEnumerable<AdvanceB>> GetAllLoansByEMPIdAndLoanTypeId(int empId, int loantypeid);
        Task<IEnumerable<AdvanceB>> GetAllNewLoansBySabhaId(int sabhaid);
        Task<(bool, string?, AdvanceB)> Create(AdvanceB newLoan, AdvanceBSettlement openSettlement, HTokenClaim token);
        Task<(bool, string?, AdvanceB)> UpdateAdvanceB(AdvanceB newLoan, HTokenClaim token);
        Task<IEnumerable<AdvanceB>> GetAllAdvanceBForSettlementSabhaId(int sabhaid, HTokenClaim token);
    }
}
