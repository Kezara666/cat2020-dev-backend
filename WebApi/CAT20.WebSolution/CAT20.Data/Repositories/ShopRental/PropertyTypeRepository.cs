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
    public class PropertyTypeRepository : Repository<PropertyType>, IPropertyTypeRepository
    {
        public PropertyTypeRepository(DbContext context) : base(context)
        {
        }

        public async Task<PropertyType> GetById(int id)
        {
            return await shopRentalDbContext.PropertyTypes
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PropertyType>> GetAll()
        {
            return
            await shopRentalDbContext.PropertyTypes
                .ToListAsync();
        }

        private ShopRentalDbContext shopRentalDbContext
        {
            get { return Context as ShopRentalDbContext; }
        }

    }
}