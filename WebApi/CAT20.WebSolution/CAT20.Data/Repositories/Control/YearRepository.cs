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
    public class YearRepository : Repository<Year>, IYearRepository
    {
        public YearRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Year>> GetAllWithYearAsync()
        {
            return await controlDbContext.Years
                .ToListAsync();
        }

        public async Task<Year> GetWithYearByIdAsync(int id)
        {
            return await controlDbContext.Years
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync(); 
        }

        public async Task<IEnumerable<Year>> GetAllWithYearByYearIdAsync(int yearId)
        {
            return await controlDbContext.Years
                .Where(m => m.ID == yearId)
                .ToListAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}