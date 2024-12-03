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
    public class VoucherVoteAllocationBalanceRepository : Repository<VoteJournalAdjustment>, IVoucherVoteAllocationBalanceRepository
    {
        public VoucherVoteAllocationBalanceRepository(DbContext context) : base(context)
        {
        }
    }
}
