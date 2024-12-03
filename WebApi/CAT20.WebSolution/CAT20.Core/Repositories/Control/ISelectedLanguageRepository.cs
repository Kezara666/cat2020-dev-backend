using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface ISelectedLanguageRepository : IRepository<SelectedLanguage>
    {
        Task<IEnumerable<SelectedLanguage>> GetAllWithSelectedLanguageAsync();
        Task<SelectedLanguage> GetWithSelectedLanguageByIdAsync(int id);
        Task<IEnumerable<SelectedLanguage>> GetAllWithSelectedLanguageBySelectedLanguageIdAsync(int Id);
        Task<SelectedLanguage> GetSelectedLanguageforSabhaIdAsync(int id);

    }
}
