using CAT20.Core.DTO.Final;
using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.FinalAccount
{
    public interface IAccountTransferRepository:IRepository<AccountTransfer>
    {
        Task<AccountTransfer> GetAccountTransferById(int id );
        Task<AccountTransfer> GetAccountTransferByVoucherId(int voucherId);
        Task<(int totalCount, IEnumerable<AccountTransfer> list)> GetAllAccountTransferForSabha(int sabhaId, bool? type, int pageNo, int pageSize, string? filterKeyWord);
    }
}
