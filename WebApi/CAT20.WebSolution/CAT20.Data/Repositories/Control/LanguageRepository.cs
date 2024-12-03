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
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        public LanguageRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Language>> GetAllWithLanguageAsync()
        {
            return await controlDbContext.Languages
                .Include(m => m.ID)
                .ToListAsync();
        }

        public async Task<Language> GetWithLanguageByIdAsync(int id)
        {
            return await controlDbContext.Languages
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Language>> GetAllWithLanguageByLanguageIdAsync(int languageId)
        {
            return await controlDbContext.Languages
                .Where(m => m.ID == languageId)
                .ToListAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}