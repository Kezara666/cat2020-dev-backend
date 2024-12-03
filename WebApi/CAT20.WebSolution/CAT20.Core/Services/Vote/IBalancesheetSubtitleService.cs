using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Vote
{
    public interface IBalancesheetSubtitleService
    {
        Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitles();
        Task<BalancesheetSubtitle> GetBalancesheetSubtitleById(int id);
        Task<BalancesheetSubtitle> CreateBalancesheetSubtitle(BalancesheetSubtitle newBalancesheetSubtitle);
        Task UpdateBalancesheetSubtitle(BalancesheetSubtitle balancesheetSubtitleToBeUpdated, BalancesheetSubtitle balancesheetSubtitle);
        Task DeleteBalancesheetSubtitle(BalancesheetSubtitle balancesheetSubtitle);

        Task<IEnumerable<BalancesheetSubtitle>> GetAllWithBalancesheetSubtitleByBalancesheetTitleId(int Id);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllWithBalancesheetSubtitleByBalancesheetTitleIdandSabhaId(int BalancesheetTitleId, int SabhaId);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForSabhaId(int SabhaId);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForTitleID(int TitleId);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForTitleIDAndAccountDetailID(int TitleId, int AccountDetailID);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForTitleClassificationID(int TitleID, int ClassificationID, int sabhaid);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesByClassificationIDMainLedgerAccountId(int ClassificationID, int mainledgeraccid, int sabhaid);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesByClassificationID(int ClassificationID, int sabhaid);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllDepositSubCategoriesForSabha(int sabhaid);
    }
}

