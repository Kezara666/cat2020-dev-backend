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
    public class SabhaRepository : Repository<Sabha>, ISabhaRepository
    {
        public SabhaRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Sabha>> GetAllWithSabhaAsync()
        {
            return await controlDbContext.Sabhas
                .Include(m => m.ID)
                .ToListAsync();
        }

        public async Task<Sabha> GetWithSabhaByIdAsync(int id)
        {
            return await controlDbContext.Sabhas
                .Where(m => m.ID == id && m.Status==1)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Sabha>> GetSabhaByDistrictId(int districtID)
        {
            return await controlDbContext.Sabhas
                .Where(m => m.DistrictID == districtID && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sabha>> GetAllWithSabhaBySabhaIdAsync(int sabhaId)
        {
            return await controlDbContext.Sabhas
                .Where(m => m.ID == sabhaId && m.Status == 1)
                .ToListAsync();
        }

        public Task<IEnumerable<Sabha>> GetAllWithProvinceByProvinceIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Sabha>> GetDistrictProvice(int sabhaId)
        {
            return await controlDbContext.Sabhas.Include(m => m.district).ThenInclude(m => m.province)
               
                .Where(m => m.ID == sabhaId && m.Status == 1).ToListAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}