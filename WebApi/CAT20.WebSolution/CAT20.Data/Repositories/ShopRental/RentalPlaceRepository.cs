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
    public class RentalPlaceRepository : Repository<RentalPlace>, IRentalPlaceRepository
    {
        public RentalPlaceRepository(DbContext context) : base(context)
        {
        }

        public async Task<RentalPlace> GetById(int id)
        {
            return await shopRentalDbContext.RentalPlaces
                .Include(f=> f.Floors)
                .ThenInclude(p=> p.Properties)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RentalPlace>> GetAll()
        {
            return
            await shopRentalDbContext.RentalPlaces
                .Include(f => f.Floors)
                .ThenInclude(p => p.Properties)
                .ToListAsync();
        }

        public async Task<IEnumerable<RentalPlace>> GetAllForOffice(int officeId)
        {
            return
                await shopRentalDbContext.RentalPlaces
                .Include(f => f.Floors)
                .ThenInclude(p => p.Properties)
                .ThenInclude(s => s.Shops)
                .Where(m => m.OfficeId == officeId)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<RentalPlace>> GetAllForSabha(int sabhaId)
        {
            return
                await shopRentalDbContext.RentalPlaces
                .Include(f => f.Floors)
                .ThenInclude(p => p.Properties)
                .ThenInclude(s => s.Shops)
                .Where(m => m.SabhaId == sabhaId)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        private ShopRentalDbContext shopRentalDbContext
        {
            get { return Context as ShopRentalDbContext; }
        }
    

    }
}