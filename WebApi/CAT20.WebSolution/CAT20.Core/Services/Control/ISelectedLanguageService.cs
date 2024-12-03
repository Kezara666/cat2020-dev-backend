using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface ISelectedLanguageService
    {
        Task<IEnumerable<SelectedLanguage>> GetAllSelectedLanguages();
        Task<SelectedLanguage> GetSelectedLanguageById(int id);
        Task<SelectedLanguage> CreateSelectedLanguage(SelectedLanguage newSelectedLanguage);
        Task UpdateSelectedLanguage(SelectedLanguage selectedLanguageToBeUpdated, SelectedLanguage selectedLanguage);
        Task DeleteSelectedLanguage(SelectedLanguage selectedLanguage);
        Task<SelectedLanguage> GetSelectedLanguageforSabhaId(int id);
    }
}

