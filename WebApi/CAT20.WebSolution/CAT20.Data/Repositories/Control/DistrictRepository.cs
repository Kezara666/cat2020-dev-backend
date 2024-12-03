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
    public class DistrictRepository : Repository<District>, IDistrictRepository
    {
        public DistrictRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<District>> GetAllWithDistrictAsync()
        {
            return await controlDbContext.Districts
                .Include(m => m.ID)
                .ToListAsync();
        }

        public async Task<District> GetWithDistrictByIdAsync(int id)
        {
            return await controlDbContext.Districts
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync(); 
        }

        public async Task<IEnumerable<District>> GetAllWithDistrictByDistrictIdAsync(int districtId)
        {
            return await controlDbContext.Districts
                .Include(m => m.ID)
                .Where(m => m.ID == districtId)
                .ToListAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}