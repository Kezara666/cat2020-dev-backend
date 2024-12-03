using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.TradeTax;

namespace CAT20.Core.Services.TradeTax
{
    public interface ITaxValueService
    {
        Task<IEnumerable<TaxValue>> GetAllTaxValues();
        Task<TaxValue> GetTaxValueById(int id);
        Task<TaxValue> CreateTaxValue(TaxValue newTaxValue);
        Task UpdateTaxValue(TaxValue taxValueToBeUpdated, TaxValue taxValue);
        Task DeleteTaxValue(TaxValue taxValue);
        Task<IEnumerable<TaxValue>> GetAllWithTaxValueByTaxValueId(int Id);
        Task<IEnumerable<TaxValue>> GetAllWithTaxValueByTaxTypeIdandSabhaId(int TaxTypeId, int SabhaId);
        Task<IEnumerable<TaxValue>> GetAllTaxValuesForSabhaId(int SabhaId);
        Task<IEnumerable<TaxValue>> GetAllTaxValuesForTaxTypeID(int TaxTypeId);
        Task<IEnumerable<TaxValue>> GetAllTaxValuesForTaxTypeIDAndSabhaID(int TaxTypeId, int sabhaid);
        Task<IEnumerable<TaxValue>> GetAllTaxValuesForSabhaID(int sabhaid);
    }
}

