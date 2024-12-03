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
    public class VoucherInvoiceRepository : Repository<VoucherInvoice>, IVoucherInvoiceRepository
    {
        public VoucherInvoiceRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<VoucherInvoice>> GetAllInvoicesForVoucher(int voucherId)
        {
            return await voteAccDbContext.VoucherInvoices.Where(x => x.VoucherId == voucherId).ToListAsync(); 
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }

    }
}
