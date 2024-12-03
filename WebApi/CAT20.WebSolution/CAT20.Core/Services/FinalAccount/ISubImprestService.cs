using CAT20.Core.DTO.Final;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Mixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface ISubImprestService
    {

        Task<(int totalCount, IEnumerable<SubImprestResource> list)> getAllSubImprestForSabha(int sabhaId, Nullable<int> officerId, Nullable<int> subImprestVoteId, int pageNo, int pageSize, string? filterKeyword,int? state);
        Task<(int totalCount, IEnumerable<SubImprestResource> list)> getAllSubImprestToSettleForSabha(int sabhaId, Nullable<int> officerId, Nullable<int> subImprestVoteId, int pageNo, int pageSize, string? filterKeyword,int? state,int? settleMethod);

        Task<SubImprestResource> GetSubImprestById(int subImprestId);

        Task<(bool,string?)> CreateUpdateSubImprest(SubImprest newSubImprest, HTokenClaim token);
        Task<(bool,string)> SettlementSubImprest(SubImprest newSubImprest, HTokenClaim token);
        Task<(bool,string,MixinOrder)> SettlementSubImprestByCash(SubImprest newSubImprest, HTokenClaim token);
    }
}
