using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.TradeTax;

namespace CAT20.Core.Services.TradeTax
{
    public interface ITradeTaxVoteAssignmentService
    {
        Task<IEnumerable<TradeTaxVoteAssignment>> GetAllTradeTaxVoteAssignments();
        Task<TradeTaxVoteAssignment> GetTradeTaxVoteAssignmentById(int id);
        Task<TradeTaxVoteAssignment> CreateTradeTaxVoteAssignment(TradeTaxVoteAssignment newTradeTaxVoteAssignment);
        Task UpdateTradeTaxVoteAssignment(TradeTaxVoteAssignment tradeTaxVoteAssignmentToBeUpdated, TradeTaxVoteAssignment tradeTaxVoteAssignment);
        Task DeleteTradeTaxVoteAssignment(TradeTaxVoteAssignment tradeTaxVoteAssignment);
        Task<IEnumerable<TradeTaxVoteAssignment>> GetAllWithTradeTaxVoteAssignmentByTradeTaxVoteAssignmentId(int Id);
        Task<IEnumerable<TradeTaxVoteAssignment>> GetAllWithTradeTaxVoteAssignmentByTaxTypeIdandSabhaId(int TaxTypeId, int SabhaId);
        Task<IEnumerable<TradeTaxVoteAssignment>> GetAllTradeTaxVoteAssignmentsForSabhaId(int SabhaId);
        Task<IEnumerable<TradeTaxVoteAssignment>> GetAllTradeTaxVoteAssignmentsForTaxTypeID(int TaxTypeId);
        Task<TradeTaxVoteAssignment> GetTradeTaxVoteAssignmentsForTaxTypeIDAndSabhaId(int TaxTypeId, int SabhaId);
    }
}

