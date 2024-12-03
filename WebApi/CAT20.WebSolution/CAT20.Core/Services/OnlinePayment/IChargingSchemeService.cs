using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.OnlienePayment;

namespace CAT20.Core.Services.OnlinePayment
{
    public interface IChargingSchemeService
    {
        Task<(bool, string?)> SaveChargingScheme(ChargingScheme newChargingScheme, HTokenClaim token);
        Task<(bool, string?)> DeleteChargingScheme(ChargingScheme chargingScheme);
        Task<ChargingScheme> GetChargingSchemeById(int id);
        Task<IEnumerable<ChargingScheme>> GetAllChargingSchemeBySubPropertyId(int SubPropertyId);
    }
}
