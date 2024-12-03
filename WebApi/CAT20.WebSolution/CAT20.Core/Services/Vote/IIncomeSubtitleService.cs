using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Vote
{
    public interface IIncomeSubtitleService
    {
        Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubtitles();
        Task<IncomeSubtitle> GetIncomeSubtitleById(int id);
        Task<IncomeSubtitle> CreateIncomeSubtitle(IncomeSubtitle newIncomeSubtitle);
        Task UpdateIncomeSubtitle(IncomeSubtitle incomeSubtitleToBeUpdated, IncomeSubtitle incomeSubtitle);
        Task DeleteIncomeSubtitle(IncomeSubtitle incomeSubtitle);

        Task<IEnumerable<IncomeSubtitle>> GetAllWithIncomeSubtitleByIncometitleId(int Id);
        Task<IEnumerable<IncomeSubtitle>> GetAllWithIncomeSubtitleByIncometitleIdSabhaId(int IncomeTitleId, int SabhaId);
        Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForSabhaId(int SabhaId);
        Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForTitleId(int TitleId);
        Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForProgrammeId(int ProgrammeId);
        Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForProgrammeClassificationId(int ProgrammeId, int ClassificationId);
        Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForTitleIDAndClassificationIDAndMainLedgerAccountId(int ClassificationID, int mainLedgerAccId, int TitleID, int sabhaId);
    }
}

