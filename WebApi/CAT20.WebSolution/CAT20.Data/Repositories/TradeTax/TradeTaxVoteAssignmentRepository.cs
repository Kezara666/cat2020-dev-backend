using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.TradeTax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.TradeTax;

namespace CAT20.Data.Repositories.TradeTax
{
    public class TradeTaxVoteAssignmentRepository : Repository<TradeTaxVoteAssignment>, ITradeTaxVoteAssignmentRepository
    {
        public TradeTaxVoteAssignmentRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<TradeTaxVoteAssignment>> GetAllWithTradeTaxVoteAssignmentAsync()
        {
            return await voteAccDbContext.TradeTaxVoteAssignments
                .Where(m => m.ActiveStatus == 1)
                .ToListAsync();
        }

        public async Task<TradeTaxVoteAssignment> GetWithTradeTaxVoteAssignmentByIdAsync(int id)
        {
            return await voteAccDbContext.TradeTaxVoteAssignments
                .Where(m => m.ID == id && m.ActiveStatus == 1)
                .FirstOrDefaultAsync(); 
        }

        public async Task<IEnumerable<TradeTaxVoteAssignment>> GetAllWithTradeTaxVoteAssignmentByTradeTaxVoteAssignmentIdAsync(int Id)
        {
            return await voteAccDbContext.TradeTaxVoteAssignments
                .Where(m => m.ID == Id && m.ActiveStatus == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<TradeTaxVoteAssignment>> GetAllWithTradeTaxVoteAssignmentByTaxTypeIdAsync(int Id)
        {
            return await voteAccDbContext.TradeTaxVoteAssignments.Where(m => m.TaxTypeID == Id && m.ActiveStatus == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<TradeTaxVoteAssignment>> GetAllWithTradeTaxVoteAssignmentByTaxTypeIdandSabhaIdAsync(int TaxTypeId, int SabhaId)
        {
            return await voteAccDbContext.TradeTaxVoteAssignments.Where(m => m.TaxTypeID == TaxTypeId && m.SabhaID == SabhaId && m.ActiveStatus == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<TradeTaxVoteAssignment>> GetAllTradeTaxVoteAssignmentsForSabhaIdAsync(int Id)
        {
            return await voteAccDbContext.TradeTaxVoteAssignments.Where(m => m.SabhaID == Id && m.ActiveStatus == 1).ToListAsync();
        }

        public async Task<IEnumerable<TradeTaxVoteAssignment>> GetAllTradeTaxVoteAssignmentsForTaxTypeIDAsync(int Id)
        {
            return await voteAccDbContext.TradeTaxVoteAssignments.Where(m => m.TaxTypeID == Id && m.ActiveStatus == 1).ToListAsync();
        }

        public async Task<TradeTaxVoteAssignment> GetTradeTaxVoteAssignmentsForTaxTypeIDAndSabhaIdAsync(int TaxTypeId, int SabhaId)
        {
            return await voteAccDbContext.TradeTaxVoteAssignments.Where(m => m.TaxTypeID == TaxTypeId && m.SabhaID == SabhaId && m.ActiveStatus == 1).FirstOrDefaultAsync();
        }
        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}