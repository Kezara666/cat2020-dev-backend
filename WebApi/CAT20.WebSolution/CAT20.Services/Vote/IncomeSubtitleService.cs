using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;

namespace CAT20.Services.Vote
{
    public class IncomeSubtitleService : IIncomeSubtitleService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        public IncomeSubtitleService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IncomeSubtitle> CreateIncomeSubtitle(IncomeSubtitle newIncomeSubtitle)
        {
            await _unitOfWork.IncomeSubtitles
                .AddAsync(newIncomeSubtitle);
            await _unitOfWork.CommitAsync();

            return newIncomeSubtitle;
        }
        public async Task DeleteIncomeSubtitle(IncomeSubtitle incomeSubtitle)
        {
            incomeSubtitle.Status = 0;
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubtitles()
        {
            return await _unitOfWork.IncomeSubtitles.GetAllAsync();
        }
        public async Task<IncomeSubtitle> GetIncomeSubtitleById(int id)
        {
            return await _unitOfWork.IncomeSubtitles.GetByIdAsync(id);
        }
        public async Task UpdateIncomeSubtitle(IncomeSubtitle incomeSubtitleToBeUpdated, IncomeSubtitle incomeSubtitle)
        {
            incomeSubtitleToBeUpdated.IncomeTitleID = incomeSubtitle.IncomeTitleID;
            incomeSubtitleToBeUpdated.NameSinhala = incomeSubtitle.NameSinhala;
            incomeSubtitleToBeUpdated.NameTamil = incomeSubtitle.NameTamil;
            incomeSubtitleToBeUpdated.NameEnglish = incomeSubtitle.NameEnglish;
            incomeSubtitleToBeUpdated.Code = incomeSubtitle.Code;

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<IncomeSubtitle>> GetAllWithIncomeSubtitleByIncometitleId(int Id)
        {
            return await _unitOfWork.IncomeSubtitles.GetAllWithIncomeSubtitleByIncometitleIdAsync(Id);
        }
        public async Task<IEnumerable<IncomeSubtitle>> GetAllWithIncomeSubtitleByIncometitleIdSabhaId(int IncomeTitleId, int SabhaId)
        {
            return await _unitOfWork.IncomeSubtitles.GetAllWithIncomeSubtitleByIncometitleIdSabhaIdAsync(IncomeTitleId, SabhaId);
        }
        public async Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForSabhaId(int SabhaId)
        {
            return await _unitOfWork.IncomeSubtitles.GetAllIncomeSubTitlesForSabhaIdAsync(SabhaId);
        }

        public async Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForTitleId(int TitleId)
        {
            return await _unitOfWork.IncomeSubtitles.GetAllIncomeSubTitlesForTitleIdAsync(TitleId);

        }
        public async Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForProgrammeId(int ProgrammeId)
        {
            return await _unitOfWork.IncomeSubtitles.GetAllIncomeSubTitlesForProgrammeIdAsync(ProgrammeId);
        }

        public async Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForProgrammeClassificationId(int ProgrammeId, int ClassificationId)
        {
            return await _unitOfWork.IncomeSubtitles.GetAllIncomeSubTitlesForProgrammeClassificationIdAsync(ProgrammeId, ClassificationId);
        }

        public async Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForTitleIDAndClassificationIDAndMainLedgerAccountId(int ClassificationID, int mainLedgerAccId, int TitleID, int sabhaId)
        {
            return await _unitOfWork.IncomeSubtitles.GetAllIncomeSubTitlesForTitleIDAndClassificationIDAndMainLedgerAccountId(ClassificationID, mainLedgerAccId, TitleID, sabhaId);
        }

    }
}