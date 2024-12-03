using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.Models.TradeTax;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.TradeTax
{
    public interface ITaxValueRepository : IRepository<TaxValue>
    {
        Task<IEnumerable<TaxValue>> GetAllWithTaxValueAsync();
        Task<TaxValue> GetWithTaxValueByIdAsync(int id);
        Task<IEnumerable<TaxValue>> GetAllWithTaxValueByTaxValueIdAsync(int Id);
        Task<IEnumerable<TaxValue>> GetAllWithTaxValueByTaxTypeIdAsync(int Id);
        Task<IEnumerable<TaxValue>> GetAllWithTaxValueByTaxTypeIdandSabhaIdAsync(int TaxTypeId, int SabhaId);
        Task<IEnumerable<TaxValue>> GetAllTaxValuesForSabhaIdAsync(int SabhaId);
        Task<IEnumerable<TaxValue>> GetAllTaxValuesForTaxTypeIDAsync(int SabhaId);
        Task<IEnumerable<TaxValue>> GetAllTaxValuesForTaxTypeIDAndSabhaIDAsync(int TaxTypeId, int sabhaid);
        Task<IEnumerable<TaxValue>> GetAllTaxValuesForSabhaIDAsync(int sabhaid);
    }
}
