using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface ILanguageRepository : IRepository<Language>
    {
        Task<IEnumerable<Language>> GetAllWithLanguageAsync();
        Task<Language> GetWithLanguageByIdAsync(int id);
        Task<IEnumerable<Language>> GetAllWithLanguageByLanguageIdAsync(int Id);
    }
}
