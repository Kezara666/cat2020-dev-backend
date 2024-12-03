using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class LanguageService : ILanguageService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public LanguageService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Language> CreateLanguage(Language newLanguage)
        {
            await _unitOfWork.Languages
                .AddAsync(newLanguage);
            await _unitOfWork.CommitAsync();

            return newLanguage;
        }
        public async Task DeleteLanguage(Language language)
        {
            _unitOfWork.Languages.Remove(language);

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<Language>> GetAllLanguages()
        {
            return await _unitOfWork.Languages.GetAllAsync();
        }
        public async Task<Language> GetLanguageById(int id)
        {
            return await _unitOfWork.Languages.GetByIdAsync(id);
        }
        public async Task UpdateLanguage(Language languageToBeUpdated, Language language)
        {
            //languageToBeUpdated.Name = language.t;

            await _unitOfWork.CommitAsync();
        }
    }
}