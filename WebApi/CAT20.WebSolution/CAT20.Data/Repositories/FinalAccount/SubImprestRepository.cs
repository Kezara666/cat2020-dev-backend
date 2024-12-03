using CAT20.Core.DTO.Final;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.FinalAccount
{
    public class SubImprestRepository : Repository<SubImprest>, ISubImprestRepository
    {
        public SubImprestRepository(DbContext context) : base(context)
        {
        }

        public async Task<SubImprest> GetSubImprestById(int subImprestId)
        {
            return await voteAccDbContext.SubImprests
                .Include(s => s.SettlementCrossOrders)
                .Include(s => s.SubImprestSettlements)
                .Where(m => m.Id == subImprestId && (m.Status == 1 || m.Status == 2 || m.Status == 3))
                .FirstOrDefaultAsync();

        }

        public async Task<(int totalCount, IEnumerable<SubImprest> list)> getAllSubImprestForSabha(int sabhaId, int? officerId, int? subImprestVoteId, int pageNo, int pageSize, string? filterKeyword, int? state)
        {
            try
            {
                FinalAccountActionStates? actionState = Enum.TryParse(state?.ToString(), out FinalAccountActionStates parsedState) ? (FinalAccountActionStates?)parsedState : null;



                var result = voteAccDbContext.SubImprests
                     .Where(m => m.SabhaId == sabhaId && m.Status == 1)
                     .Where(a => officerId.HasValue ? a.EmployeeId == officerId.Value : true)
                     .Where(a => subImprestVoteId.HasValue ? a.SubImprestVoteId == subImprestVoteId.Value : true)
                     .Where(a => state.HasValue ? a.ActionStates == actionState : a.ActionStates != FinalAccountActionStates.Settled)
                    .OrderByDescending(m => m.Id);


                int totalCount = await result.CountAsync();


                //var pageSize = 10;
                int skipAmount = (pageNo - 1) * pageSize;

                var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


                return (totalCount, list);
            }catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<(int totalCount, IEnumerable<SubImprest> list)> getAllSubImprestToSettleForSabha(int sabhaId, int? officerId, int? subImprestVoteId, int pageNo, int pageSize, string? filterKeyword, int? state, int? status)
        {
            try
            {
                FinalAccountActionStates? actionState = Enum.TryParse(state?.ToString(), out FinalAccountActionStates parsedState) ? (FinalAccountActionStates?)parsedState : null;


                    
                  /*status (1, 2  setttle by cash , 3 by bills ) */

                var result = from subImprest in voteAccDbContext.SubImprests
                            join voucher in voteAccDbContext.Voucher
                            on subImprest.VoucherId equals voucher.Id
                            where subImprest.SabhaId == sabhaId
                                  && (status.HasValue ? status.Value == subImprest.Status : subImprest.Status == 1)
                                  && (!officerId.HasValue || subImprest.EmployeeId == officerId.Value)
                                  && (!subImprestVoteId.HasValue || subImprest.SubImprestVoteId == subImprestVoteId.Value)
                                  && (!state.HasValue || subImprest.ActionStates == actionState || subImprest.ActionStates != FinalAccountActionStates.Settled)
                                  && voucher.ActionState == FinalAccountActionStates.HasCheque 
                            orderby subImprest.Id descending
                            select subImprest;

                int totalCount = await result.CountAsync();


                //var pageSize = 10;
                int skipAmount = (pageNo - 1) * pageSize;

                var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


                return (totalCount, list);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}
