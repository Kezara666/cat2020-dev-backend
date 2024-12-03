using CAT20.Core.Models.ShopRental;
using CAT20.Core.Repositories.ShopRental;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.ShopRental
{
    public class FineChargingMethodRepository : Repository<FineChargingMethod>, IFineChargingMethodRepository
    {
        public FineChargingMethodRepository(DbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<FineChargingMethod>> GetAll()
        {
            return
            await shopRentalDbContext.FineChargingMethod
                .ToListAsync();
        }

        private ShopRentalDbContext shopRentalDbContext
        {
            get { return Context as ShopRentalDbContext; }
        }
    }
}
