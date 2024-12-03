using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;

namespace CAT20.Services.Vote
{
    public class BalancesheetSubtitleService : IBalancesheetSubtitleService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        public BalancesheetSubtitleService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BalancesheetSubtitle> CreateBalancesheetSubtitle(BalancesheetSubtitle newBalancesheetSubtitle)
        {
            await _unitOfWork.BalancesheetSubtitles
                .AddAsync(newBalancesheetSubtitle);
            await _unitOfWork.CommitAsync();

            return newBalancesheetSubtitle;
        }
        public async Task DeleteBalancesheetSubtitle(BalancesheetSubtitle balancesheetSubtitle)
        {
            balancesheetSubtitle.Status = 0;
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitles()
        {
            return await _unitOfWork.BalancesheetSubtitles.GetAllAsync();
        }
        public async Task<BalancesheetSubtitle> GetBalancesheetSubtitleById(int id)
        {
            return await _unitOfWork.BalancesheetSubtitles.GetByIdAsync(id);
        }
        public async Task UpdateBalancesheetSubtitle(BalancesheetSubtitle balancesheetSubtitleToBeUpdated, BalancesheetSubtitle balancesheetSubtitle)
        {
            balancesheetSubtitleToBeUpdated.BalsheetTitleID = balancesheetSubtitle.BalsheetTitleID;
            balancesheetSubtitleToBeUpdated.NameSinhala = balancesheetSubtitle.NameSinhala;
            balancesheetSubtitleToBeUpdated.NameTamil = balancesheetSubtitle.NameTamil;
            balancesheetSubtitleToBeUpdated.NameEnglish = balancesheetSubtitle.NameEnglish;
            balancesheetSubtitleToBeUpdated.Code = balancesheetSubtitle.Code;
            balancesheetSubtitleToBeUpdated.BankAccountID = balancesheetSubtitle.BankAccountID;

            await _unitOfWork.CommitAsync();
        }


        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllWithBalancesheetSubtitleByBalancesheetTitleId(int Id)
        {
            return await _unitOfWork.BalancesheetSubtitles.GetAllWithBalancesheetSubtitleByBalancesheetTitleIdAsync(Id);
        }
        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllWithBalancesheetSubtitleByBalancesheetTitleIdandSabhaId(int BalancesheetTitleId, int SabhaId)
        {
            return await _unitOfWork.BalancesheetSubtitles.GetAllWithBalancesheetSubtitleByBalancesheetTitleIdandSabhaIdAsync(BalancesheetTitleId, SabhaId);
        }

        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForSabhaId(int SabhaId)
        {
            return await _unitOfWork.BalancesheetSubtitles.GetAllBalancesheetSubtitlesForSabhaIdAsync(SabhaId);
        }

        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForTitleID(int TitleID)
        {
            return await _unitOfWork.BalancesheetSubtitles.GetAllBalancesheetSubtitlesForTitleIDAsync(TitleID);
        }

        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForTitleIDAndAccountDetailID(int TitleID, int accountdetailid)
        {
            return await _unitOfWork.BalancesheetSubtitles.GetAllBalancesheetSubtitlesForTitleIDAndAccountDetailIDAsync(TitleID, accountdetailid);
        }

        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForTitleClassificationID(int TitleID, int ClassificationID, int sabhaid)
        {
            return await _unitOfWork.BalancesheetSubtitles.GetAllBalancesheetSubtitlesForTitleClassificationIDAsync(TitleID, ClassificationID, sabhaid);
        }
        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesByClassificationIDMainLedgerAccountId(int ClassificationID, int mainledgeraccid, int sabhaid)
        {
            return await _unitOfWork.BalancesheetSubtitles.GetAllBalancesheetSubtitlesByClassificationIDMainLedgerAccountId(ClassificationID, mainledgeraccid, sabhaid);
        }

        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesByClassificationID(int ClassificationID, int sabhaid)
        {
            return await _unitOfWork.BalancesheetSubtitles.GetAllBalancesheetSubtitlesForClassificationIDAsync(ClassificationID, sabhaid);
        }

        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllDepositSubCategoriesForSabha(int sabhaid)
        {
            return await _unitOfWork.BalancesheetSubtitles.GetAllDepositSubCategoriesForSabha(sabhaid);
        }
    }
}