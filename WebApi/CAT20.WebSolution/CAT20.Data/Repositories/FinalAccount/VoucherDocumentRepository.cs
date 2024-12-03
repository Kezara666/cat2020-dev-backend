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
    public class VoucherDocumentRepository : Repository<VoucherDocument>, IVoucherDocumentRepository
    {
        public VoucherDocumentRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<VoucherDocument>> GetAllDocumentsForVoucher(int voucherId)
        {
            return await voteAccDbContext.VoucherDocuments.Where(x => x.VoucherId == voucherId).ToListAsync();
        }


        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
    
}
