using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class SelectedLanguageService : ISelectedLanguageService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public SelectedLanguageService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<SelectedLanguage> CreateSelectedLanguage(SelectedLanguage newSelectedLanguage)
        {
            await _unitOfWork.SelectedLanguages
                .AddAsync(newSelectedLanguage);
            await _unitOfWork.CommitAsync();

            return newSelectedLanguage;
        }

        //public async Task<SelectedLanguage> CreateSelectedLanguage(SelectedLanguage newSelectedLanguage)
        //{
        //    try
        //    {
        //        var selectedLanguage = await _unitOfWork.SelectedLanguages.GetByIdAsync(newSelectedLanguage.ID!);
        //        if (selectedLanguage != null)
        //        {;
        //            selectedLanguage.SabhaID= newSelectedLanguage.SabhaID;
        //            selectedLanguage.LanguageID= newSelectedLanguage.LanguageID;
        //        }
        //        else
        //        {
        //            await _unitOfWork.SelectedLanguages.AddAsync(newSelectedLanguage);

        //        }
        //        await _unitOfWork.CommitAsync();

        //        return newSelectedLanguage;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}






        public async Task DeleteSelectedLanguage(SelectedLanguage selectedLanguage)
        {
            _unitOfWork.SelectedLanguages.Remove(selectedLanguage);

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<SelectedLanguage>> GetAllSelectedLanguages()
        {
            return await _unitOfWork.SelectedLanguages.GetAllAsync();
        }
        public async Task<SelectedLanguage> GetSelectedLanguageById(int id)
        {
            return await _unitOfWork.SelectedLanguages.GetByIdAsync(id);
        }

        public async Task<SelectedLanguage> GetSelectedLanguageforSabhaId(int id)
        {
            return await _unitOfWork.SelectedLanguages.GetSelectedLanguageforSabhaIdAsync(id);
        }


        public async Task UpdateSelectedLanguage(SelectedLanguage selectedLanguageToBeUpdated, SelectedLanguage selectedLanguage)
        {
            //selectedLanguageToBeUpdated.Name = selectedLanguage.t;
            selectedLanguageToBeUpdated.SabhaID = selectedLanguage.SabhaID;
            selectedLanguageToBeUpdated.LanguageID= selectedLanguage.LanguageID;

            await _unitOfWork.CommitAsync();
        }
    }
}