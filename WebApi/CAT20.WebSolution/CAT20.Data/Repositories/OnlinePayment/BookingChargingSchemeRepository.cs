using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Repositories.OnlinePayment;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.OnlinePayment
{
    public class BookingChargingSchemeRepository : Repository<ChargingScheme>, IChargingSchemeRepository
    {
        public BookingChargingSchemeRepository(DbContext context) : base(context) { }
        private OnlinePaymentDbContext onlinePaymentDbContext
        {
            get { return Context as OnlinePaymentDbContext; }
        }
        public async Task<IEnumerable<ChargingScheme>> GetAllChargingSchemeForSubProprtyIdAsync(int subPropertyId)
        {
            return await onlinePaymentDbContext.ChargingScheme.Where(b => b.SubPropertyId == subPropertyId && b.Status == 1).ToListAsync();
        }

        public async Task<ChargingScheme> GetChargingSchemeByIdAsync(int id)
        {
            return onlinePaymentDbContext.ChargingScheme.Where(b => b.ID == id && b.Status == 1).FirstOrDefault();
        }
    }
}
