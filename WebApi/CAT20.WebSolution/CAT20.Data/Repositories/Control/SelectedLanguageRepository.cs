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
    public class SelectedLanguageRepository : Repository<SelectedLanguage>, ISelectedLanguageRepository
    {
        public SelectedLanguageRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<SelectedLanguage>> GetAllWithSelectedLanguageAsync()
        {
            return await controlDbContext.SelectedLanguages
                .ToListAsync();
        }

        public async Task<SelectedLanguage> GetWithSelectedLanguageByIdAsync(int id)
        {
            return await controlDbContext.SelectedLanguages
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<SelectedLanguage>> GetAllWithSelectedLanguageBySelectedLanguageIdAsync(int selectedLanguageId)
        {
            return await controlDbContext.SelectedLanguages
                .Where(m => m.ID == selectedLanguageId)
                .ToListAsync();
        }

        public async Task<SelectedLanguage> GetSelectedLanguageforSabhaIdAsync(int sabhaId)
        {
            return await controlDbContext.SelectedLanguages
                .Where(m => m.SabhaID == sabhaId)
                .FirstOrDefaultAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}