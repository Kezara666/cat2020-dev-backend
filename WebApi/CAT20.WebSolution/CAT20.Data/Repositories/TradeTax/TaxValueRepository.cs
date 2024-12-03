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
    public class TaxValueRepository : Repository<TaxValue>, ITaxValueRepository
    {
        public TaxValueRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<TaxValue>> GetAllWithTaxValueAsync()
        {
            return await voteAccDbContext.TaxValues
                .Where(m => m.ActiveStatus == 1)
                .ToListAsync();
        }

        public async Task<TaxValue> GetWithTaxValueByIdAsync(int id)
        {
            return await voteAccDbContext.TaxValues
                .Where(m => m.ID == id && m.ActiveStatus == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TaxValue>> GetAllWithTaxValueByTaxValueIdAsync(int Id)
        {
            return await voteAccDbContext.TaxValues
                .Where(m => m.ID == Id && m.ActiveStatus == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<TaxValue>> GetAllWithTaxValueByTaxTypeIdAsync(int Id)
        {
            return await voteAccDbContext.TaxValues.Where(m => m.TaxTypeID == Id && m.ActiveStatus == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<TaxValue>> GetAllWithTaxValueByTaxTypeIdandSabhaIdAsync(int TaxTypeId, int SabhaId)
        {
            return await voteAccDbContext.TaxValues.Where(m => m.TaxTypeID == TaxTypeId && m.SabhaID == SabhaId && m.ActiveStatus == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaxValue>> GetAllTaxValuesForSabhaIdAsync(int Id)
        {
            return await voteAccDbContext.TaxValues.Where(m => m.SabhaID == Id && m.ActiveStatus == 1).ToListAsync();
        }

        public async Task<IEnumerable<TaxValue>> GetAllTaxValuesForTaxTypeIDAsync(int Id)
        {
            return await voteAccDbContext.TaxValues.Where(m => m.TaxTypeID == Id && m.ActiveStatus == 1).ToListAsync();
        }
        public async Task<IEnumerable<TaxValue>> GetAllTaxValuesForTaxTypeIDAndSabhaIDAsync(int TaxTypeId, int sabhaid)
        {
            return await voteAccDbContext.TaxValues.Where(m => m.TaxTypeID == TaxTypeId && m.SabhaID == sabhaid && m.ActiveStatus == 1).ToListAsync();
        }
        public async Task<IEnumerable<TaxValue>> GetAllTaxValuesForSabhaIDAsync(int sabhaid)
        {
            return await voteAccDbContext.TaxValues.Where(m => m.SabhaID == sabhaid && m.ActiveStatus == 1).ToListAsync();
        }
        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}