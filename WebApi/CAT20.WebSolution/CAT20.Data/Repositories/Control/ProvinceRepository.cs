using CAT20.Core.Models.Control;
using CAT20.Core.Repositories.Control;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Control
{
    public class ProvinceRepository : Repository<Province>, IProvinceRepository
    {
        public ProvinceRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Province>> GetAllWithProvinceAsync()
        {
            return await controlDbContext.Provinces
                .ToListAsync();
        }

        public async Task<Province> GetWithProvinceByIdAsync(int id)
        {
            return await controlDbContext.Provinces
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync(); 
        }

        public async Task<IEnumerable<Province>> GetAllWithProvinceByProvinceIdAsync(int provinceId)
        {
            return await controlDbContext.Provinces
                .Where(m => m.ID == provinceId)
                .ToListAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}