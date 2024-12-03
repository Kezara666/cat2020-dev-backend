using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface ITaxTypeService
    {
        Task<IEnumerable<TaxType>> GetAllTaxTypes();
        Task<IEnumerable<TaxType>> GetAllBasicTaxTypes();
        Task<TaxType> GetTaxTypeById(int id);
        Task<TaxType> CreateTaxType(TaxType newTaxType);
        Task UpdateTaxType(TaxType taxtypeToBeUpdated, TaxType taxtype);
        Task DeleteTaxType(TaxType taxtype);
    }
}

