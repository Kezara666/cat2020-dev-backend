using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.TradeTax;

namespace CAT20.Core.Services.TradeTax
{
    public interface IBusinessSubNatureService
    {
        Task<IEnumerable<BusinessSubNature>> GetAllBusinessSubNatures();
        Task<BusinessSubNature> GetBusinessSubNatureById(int id);
        Task<BusinessSubNature> CreateBusinessSubNature(BusinessSubNature newBusinessSubNature);
        Task UpdateBusinessSubNature(BusinessSubNature businessSubNatureToBeUpdated, BusinessSubNature businessSubNature);
        Task DeleteBusinessSubNature(BusinessSubNature businessSubNature);
        Task<IEnumerable<BusinessSubNature>> GetAllWithBusinessSubNatureByBusinessSubNatureId(int Id);
        Task<IEnumerable<BusinessSubNature>> GetAllWithBusinessSubNatureByBusinessNatureIdandSabhaId(int BusinessNatureId, int SabhaId);
        Task<IEnumerable<BusinessSubNature>> GetAllBusinessSubNaturesForSabhaId(int SabhaId);
        Task<IEnumerable<BusinessSubNature>> GetAllBusinessSubNaturesForBusinessNatureID(int BusinessNatureId);

    }
}

