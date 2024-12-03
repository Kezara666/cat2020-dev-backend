using CAT20.Core.DTO.Final;
using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.FinalAccount
{
    public interface ISubImprestRepository:IRepository<SubImprest>
    {
        Task<SubImprest> GetSubImprestById(int subImprestId);
        Task<(int totalCount, IEnumerable<SubImprest> list)> getAllSubImprestForSabha(int sabhaId, Nullable<int> officerId, Nullable<int> subImprestVoteId, int pageNo, int pageSize, string? filterKeyword,int? state);
        Task<(int totalCount, IEnumerable<SubImprest> list)> getAllSubImprestToSettleForSabha(int sabhaId, Nullable<int> officerId, Nullable<int> subImprestVoteId, int pageNo, int pageSize, string? filterKeyword,int? state,int? status);
    }
}
