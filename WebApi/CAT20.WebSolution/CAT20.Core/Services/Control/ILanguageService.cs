using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface ILanguageService
    {
        Task<IEnumerable<Language>> GetAllLanguages();
        Task<Language> GetLanguageById(int id);
        Task<Language> CreateLanguage(Language newLanguage);
        Task UpdateLanguage(Language languageToBeUpdated, Language language);
        Task DeleteLanguage(Language language);
    }
}

