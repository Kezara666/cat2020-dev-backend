using CAT20.Core.Models.TradeTax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.TradeTax
{
    public interface IBusinessNatureService
    {
        Task<IEnumerable<BusinessNature>> GetAllBusinessNatures();
        Task<BusinessNature> GetBusinessNatureById(int id);
        Task<BusinessNature> CreateBusinessNature(BusinessNature newBusinessNature);
        Task UpdateBusinessNature(BusinessNature businessNatureToBeUpdated, BusinessNature businessNature);
        Task DeleteBusinessNature(BusinessNature businessNature);
        Task<IEnumerable<BusinessNature>> GetAllWithBusinessNatureByBusinessNatureId(int Id);
        Task<IEnumerable<BusinessNature>> GetAllBusinessNatureBySabhaId(int Id);
    }
}

