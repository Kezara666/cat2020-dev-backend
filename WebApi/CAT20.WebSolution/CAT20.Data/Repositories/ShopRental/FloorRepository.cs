using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Interfaces;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Repositories.ShopRental;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.ShopRental
{
    public class FloorRepository : Repository<Floor>, IFloorRepository
    {
        public FloorRepository(DbContext context) : base(context)
        {
        }

        public async Task<Floor> GetById(int id)
        {
            return await shopRentalDbContext.Floors
                .Include(r => r.RentalPlace)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Floor>> GetAll()
        {
            return
            await shopRentalDbContext.Floors
                 .Include(r => r.RentalPlace)
                .ToListAsync();
        }

        public async Task<IEnumerable<Floor>> GetAllForRentalPlace(int rentalplaceId)
        {
            return
                await shopRentalDbContext.Floors
                .Include(r => r.RentalPlace)
                .Include(p=>p.Properties)
                .Where(m => m.RentalPlaceId == rentalplaceId)
                .ToListAsync();
        }

        private ShopRentalDbContext shopRentalDbContext
        {
            get { return Context as ShopRentalDbContext; }
        }

    }
}