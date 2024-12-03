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
    public class AppCategoryRepository : Repository<AppCategory>, IAppCategoryRepository
    {
        public AppCategoryRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<AppCategory>> GetAllWithAppCategoryAsync()
        {
            return await controlDbContext.AppCategories
                .Include(m => m.ID)
                .ToListAsync();
        }

        public async Task<AppCategory> GetWithAppCategoryByIdAsync(int id)
        {
            return await controlDbContext.AppCategories
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync(); 
        }

        public async Task<IEnumerable<AppCategory>> GetAllWithAppCategoryByAppCategoryIdAsync(int appCategoryId)
        {
            return await controlDbContext.AppCategories
                .Include(m => m.ID)
                .Where(m => m.ID == appCategoryId)
                .ToListAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}