using CAT20.Core.Models.Control;
using CAT20.Core.Repositories.Control;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Control
{
    public class BankBranchRepository : Repository<BankBranch>, IBankBranchRepository
    {
        public BankBranchRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<BankBranch>> GetAllBankBranches()
        {
            return await controlDbContext.BankBranches.Where(m => m.Status == 1)
                .ToListAsync();
        }

        public async Task<BankBranch> GetBankBranchById(int id)
        {
            return await controlDbContext.BankBranches
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync();
        }

        public async Task<BankBranch> GetBankBranchByBankCodeAndBranchCode(int bankcode, int branchcode)
        {
            return await controlDbContext.BankBranches
                .Where(m => m.BankCode == bankcode && m.BranchCode==branchcode.ToString())
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BankBranch>> GetAllBankBranchesForBankCode(int bankcode)
        {


            try
            {

                return await controlDbContext.BankBranches
                    .Where(m => m.BankCode == bankcode)
                    .ToListAsync();
            }
            catch (Exception ex) { 
                throw(ex);
            }

        }

        public Task<BankBranch> GetBankBranchWithBankById(int branchId)
        {
            return controlDbContext.BankBranches
                .Include(m => m.Bank)
                .Where(m => m.ID == branchId)
                .FirstOrDefaultAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}