using CAT20.Core.Models.Control;
using CAT20.Core.Models.TradeTax;
using CAT20.Core.Repositories.TradeTax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.TradeTax
{
    public class BusinessNatureRepository : Repository<BusinessNature>, IBusinessNatureRepository
    {
        public BusinessNatureRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<BusinessNature>> GetAllWithBusinessNatureAsync()
        {
            return await voteAccDbContext.BusinessNatures
                .Where(m => m.ActiveStatus == 1)
                .ToListAsync();
        }

        public async Task<BusinessNature> GetWithBusinessNatureByIdAsync(int id)
        {
            return await voteAccDbContext.BusinessNatures
                .Where(m => m.ID == id && m.ActiveStatus == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BusinessNature>> GetAllWithBusinessNatureByBusinessNatureIdAsync(int Id)
        {
            return await voteAccDbContext.BusinessNatures
                .Where(m => m.ID == Id && m.ActiveStatus == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<BusinessNature>> GetAllWithBusinessNatureBySabhaIdAsync(int SabahId)
        {
            return await voteAccDbContext.BusinessNatures.Where(m => m.SabhaID == SabahId && m.ActiveStatus == 1)
                .ToListAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}