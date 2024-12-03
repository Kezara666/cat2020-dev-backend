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
    public class FineCalTypeRepository : Repository<FineCalType>, IFineCalTypeRepository
    {
        public FineCalTypeRepository(DbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<FineCalType>> GetAll()
        {
            return
            await shopRentalDbContext.FineCalType
                .ToListAsync();
        }

        private ShopRentalDbContext shopRentalDbContext
        {
            get { return Context as ShopRentalDbContext; }
        }
    }
}
