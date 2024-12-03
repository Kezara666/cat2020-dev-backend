using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface ITaxTypeRepository : IRepository<TaxType>
    {
        Task<IEnumerable<TaxType>> GetAllBasicTaxTypesAsync();
        Task<IEnumerable<TaxType>> GetAllWithTaxTypeAsync();
        Task<TaxType> GetWithTaxTypeByIdAsync(int id);
    }
}
