using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;

namespace CAT20.Core.Repositories.Vote
{
    public interface IIncomeSubtitleRepository : IRepository<IncomeSubtitle>
    {
        Task<IEnumerable<IncomeSubtitle>> GetAllWithIncomeSubtitleAsync();
        Task<IncomeSubtitle> GetWithIncomeSubtitleByIdAsync(int id);
        Task<IEnumerable<IncomeSubtitle>> GetAllWithIncomeSubtitleByIncomeSubtitleIdAsync(int Id);
        Task<IEnumerable<IncomeSubtitle>> GetAllWithIncomeSubtitleByIncometitleIdAsync(int Id);
        Task<IEnumerable<IncomeSubtitle>> GetAllWithIncomeSubtitleByIncometitleIdSabhaIdAsync(int IncomeTitleId, int SabhaId);
        Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForSabhaIdAsync(int Id);
        Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForTitleIdAsync(int Id);
        Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForProgrammeIdAsync(int Id);
        Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForProgrammeClassificationIdAsync(int programmeId, int classificationId);
        Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForTitleIDAndClassificationIDAndMainLedgerAccountId(int ClassificationID, int mainLedgerAccId, int TitleID, int sabhaId);
    }
}
