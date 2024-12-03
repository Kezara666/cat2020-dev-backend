using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.Models.TradeTax;

namespace CAT20.Core.Repositories.TradeTax
{
    public interface IBusinessSubNatureRepository : IRepository<BusinessSubNature>
    {
        Task<IEnumerable<BusinessSubNature>> GetAllWithBusinessSubNatureAsync();
        Task<BusinessSubNature> GetWithBusinessSubNatureByIdAsync(int id);
        Task<IEnumerable<BusinessSubNature>> GetAllWithBusinessSubNatureByBusinessSubNatureIdAsync(int Id);
        Task<IEnumerable<BusinessSubNature>> GetAllWithBusinessSubNatureByBusinessNatureIdAsync(int Id);
        Task<IEnumerable<BusinessSubNature>> GetAllWithBusinessSubNatureByBusinessNatureIdandSabhaIdAsync(int BusinessNatureId, int SabhaId);
        Task<IEnumerable<BusinessSubNature>> GetAllBusinessSubNaturesForSabhaIdAsync(int SabhaId);
        Task<IEnumerable<BusinessSubNature>> GetAllBusinessSubNaturesForBusinessNatureIDAsync(int SabhaId);
    }
}
