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
    public class TaxTypeRepository : Repository<TaxType>, ITaxTypeRepository
    {
        public TaxTypeRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<TaxType>> GetAllWithTaxTypeAsync()
        {
            return await controlDbContext.TaxTypes
                .ToListAsync();
        }

        public async Task<IEnumerable<TaxType>> GetAllBasicTaxTypesAsync()
        {
            return await controlDbContext.TaxTypes
                .Where(m => m.IsMainTax==1)
                .ToListAsync();
        }

        public async Task<TaxType> GetWithTaxTypeByIdAsync(int id)
        {
            return await controlDbContext.TaxTypes
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync(); 
        }

        public async Task<IEnumerable<TaxType>> GetAllWithTaxTypeByTaxTypeIdAsync(int taxtypeId)
        {
            return await controlDbContext.TaxTypes
                .Where(m => m.ID == taxtypeId)
                .ToListAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}