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
    public class RentalPaymentDateTypeRepository : Repository<RentalPaymentDateType>, IRentalPaymentDateTypeRepository
    {
        public RentalPaymentDateTypeRepository(DbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<RentalPaymentDateType>> GetAll()
        {
            return
            await shopRentalDbContext.RentalPaymentDateType
                .ToListAsync();
        }

        private ShopRentalDbContext shopRentalDbContext
        {
            get { return Context as ShopRentalDbContext; }
        }
    }
}
