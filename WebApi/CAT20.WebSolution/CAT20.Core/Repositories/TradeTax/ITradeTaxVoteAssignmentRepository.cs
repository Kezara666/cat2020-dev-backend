using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.Models.TradeTax;

namespace CAT20.Core.Repositories.TradeTax
{
    public interface ITradeTaxVoteAssignmentRepository : IRepository<TradeTaxVoteAssignment>
    {
        Task<IEnumerable<TradeTaxVoteAssignment>> GetAllWithTradeTaxVoteAssignmentAsync();
        Task<TradeTaxVoteAssignment> GetWithTradeTaxVoteAssignmentByIdAsync(int id);
        Task<IEnumerable<TradeTaxVoteAssignment>> GetAllWithTradeTaxVoteAssignmentByTradeTaxVoteAssignmentIdAsync(int Id);
        Task<IEnumerable<TradeTaxVoteAssignment>> GetAllWithTradeTaxVoteAssignmentByTaxTypeIdAsync(int Id);
        Task<IEnumerable<TradeTaxVoteAssignment>> GetAllWithTradeTaxVoteAssignmentByTaxTypeIdandSabhaIdAsync(int TaxTypeId, int SabhaId);
        Task<IEnumerable<TradeTaxVoteAssignment>> GetAllTradeTaxVoteAssignmentsForSabhaIdAsync(int SabhaId);
        Task<IEnumerable<TradeTaxVoteAssignment>> GetAllTradeTaxVoteAssignmentsForTaxTypeIDAsync(int SabhaId);
        Task<TradeTaxVoteAssignment> GetTradeTaxVoteAssignmentsForTaxTypeIDAndSabhaIdAsync(int TaxTypeId, int SabhaId);
    }
}
