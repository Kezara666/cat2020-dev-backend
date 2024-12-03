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
    public class PropertyNatureRepository : Repository<PropertyNature>, IPropertyNatureRepository
    {
        public PropertyNatureRepository(DbContext context) : base(context)
        {
        }

        public async Task<PropertyNature> GetById(int id)
        {
            return await shopRentalDbContext.PropertyNatures
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PropertyNature>> GetAll()
        {
            return
            await shopRentalDbContext.PropertyNatures
                .ToListAsync();
        }

        public async Task<IEnumerable<PropertyNature>> GetAllForSabha(int sabhaId)
        {
            return
                await shopRentalDbContext.PropertyNatures
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