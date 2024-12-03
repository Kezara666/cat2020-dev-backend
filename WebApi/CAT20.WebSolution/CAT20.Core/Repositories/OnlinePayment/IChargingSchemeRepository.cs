using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.OnlienePayment;


namespace CAT20.Core.Repositories.OnlinePayment
{
    public interface IChargingSchemeRepository : IRepository<ChargingScheme>
    {
        Task<IEnumerable<ChargingScheme>> GetAllChargingSchemeForSubProprtyIdAsync(int subProprtyId);
        Task<ChargingScheme> GetChargingSchemeByIdAsync(int id);
    }
}
