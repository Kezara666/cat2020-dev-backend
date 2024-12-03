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
    public class OfficeRepository : Repository<Office>, IOfficeRepository
    {
        public OfficeRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Office>> GetAllWithOfficeAsync()
        {
            return await controlDbContext.Offices
                .ToListAsync();
        }

        public async Task<Office> GetWithOfficeByIdAsync(int id)
        {
            return await controlDbContext.Offices
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Office> GetOfficeByIdWithSabhaDetails(int id)
        {
            return await controlDbContext.Offices
                .Include(o=>o.sabha)
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Office> GetByIdAsync(int? id)
        {
            return await controlDbContext.Offices
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Office>> GetAllWithOfficeBySabhaIdAsync(int sabhaId)
        {
            return await controlDbContext.Offices
                .Where(m => m.SabhaID == sabhaId)
                .ToListAsync();
        }

        public async Task<List<int?>> GetAllWithOfficeIdsBySabhaIdAsync(int sabhaId)
        {
                return await controlDbContext.Offices
                .Where(m => m.SabhaID == sabhaId)
                .Select(m=> m.ID)
                .ToListAsync();
        }

        public async Task<IEnumerable<Office>> GetAllWithOfficeBySabhaIdAndOfficeTypeAsync(int sabhaId, int otype)
        {
            return await controlDbContext.Offices
                .Where(m => m.SabhaID == sabhaId && m.OfficeTypeID==otype)
                .ToListAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}