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
    public class FineRateTypeRepository :Repository<FineRateType>, IFineRateTypeRepository
    {
        public FineRateTypeRepository(DbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<FineRateType>> GetAll()
        {
            return
            await shopRentalDbContext.FineRateType
                .ToListAsync();
        }

        private ShopRentalDbContext shopRentalDbContext
        {
            get { return Context as ShopRentalDbContext; }
        }
    }
}
