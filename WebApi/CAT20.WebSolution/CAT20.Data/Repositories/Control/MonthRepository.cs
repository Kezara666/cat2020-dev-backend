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
    public class MonthRepository : Repository<Month>, IMonthRepository
    {
        public MonthRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Month>> GetAllWithMonthAsync()
        {
            return await controlDbContext.Months
                .ToListAsync();
        }

        public async Task<Month> GetWithMonthByIdAsync(int id)
        {
            return await controlDbContext.Months
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Month>> GetAllWithMonthByMonthIdAsync(int monthId)
        {
            return await controlDbContext.Months
                .Where(m => m.ID == monthId)
                .ToListAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}