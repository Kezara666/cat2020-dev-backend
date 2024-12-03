using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;

namespace CAT20.Core.Repositories.Vote
{
    public interface IBalancesheetSubtitleRepository : IRepository<BalancesheetSubtitle>
    {
        Task<IEnumerable<BalancesheetSubtitle>> GetAllWithBalancesheetSubtitleAsync();
        Task<BalancesheetSubtitle> GetWithBalancesheetSubtitleByIdAsync(int id);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllWithBalancesheetSubtitleByBalancesheetSubtitleIdAsync(int Id);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllWithBalancesheetSubtitleByBalancesheetTitleIdAsync(int Id);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllWithBalancesheetSubtitleByBalancesheetTitleIdandSabhaIdAsync(int BalancesheetTitleId, int SabhaId);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForSabhaIdAsync(int SabhaId);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForTitleIDAsync(int SabhaId);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForTitleIDAndAccountDetailIDAsync(int TitleID, int accountdetailid);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForTitleClassificationIDAsync(int TitleID, int ClassificationID, int sabhaid);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForClassificationIDAsync(int ClassificationID, int sabhaid);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesByClassificationIDMainLedgerAccountId(int ClassificationID, int mainledgeraccid, int sabhaid);
        Task<IEnumerable<BalancesheetSubtitle>> GetAllDepositSubCategoriesForSabha(int sabhaid);
    }
}
