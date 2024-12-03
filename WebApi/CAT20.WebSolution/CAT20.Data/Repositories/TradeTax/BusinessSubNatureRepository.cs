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
    public class BusinessSubNatureRepository : Repository<BusinessSubNature>, IBusinessSubNatureRepository
    {
        public BusinessSubNatureRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<BusinessSubNature>> GetAllWithBusinessSubNatureAsync()
        {
            return await voteAccDbContext.BusinessSubNatures
                .Where(m => m.ActiveStatus == 1)
                .ToListAsync();
        }

        public async Task<BusinessSubNature> GetWithBusinessSubNatureByIdAsync(int id)
        {
            return await voteAccDbContext.BusinessSubNatures
                .Where(m => m.ID == id &&  m.ActiveStatus == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BusinessSubNature>> GetAllWithBusinessSubNatureByBusinessSubNatureIdAsync(int Id)
        {
            return await voteAccDbContext.BusinessSubNatures
                .Where(m => m.ID == Id && m.ActiveStatus == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<BusinessSubNature>> GetAllWithBusinessSubNatureByBusinessNatureIdAsync(int Id)
        {
            return await voteAccDbContext.BusinessSubNatures.Where(m => m.BusinessNatureID == Id && m.ActiveStatus == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<BusinessSubNature>> GetAllWithBusinessSubNatureByBusinessNatureIdandSabhaIdAsync(int BusinessNatureId, int SabhaId)
        {
            return await voteAccDbContext.BusinessSubNatures.Where(m => m.BusinessNatureID == BusinessNatureId && m.SabhaID == SabhaId && m.ActiveStatus == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusinessSubNature>> GetAllBusinessSubNaturesForSabhaIdAsync(int Id)
        {
            return await voteAccDbContext.BusinessSubNatures.Where(m => m.SabhaID == Id && m.ActiveStatus == 1).ToListAsync();
        }

        public async Task<IEnumerable<BusinessSubNature>> GetAllBusinessSubNaturesForBusinessNatureIDAsync(int Id)
        {
            return await voteAccDbContext.BusinessSubNatures.Where(m => m.BusinessNatureID == Id && m.ActiveStatus == 1).ToListAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}