using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;

namespace CAT20.Core.Services.Vote
{
    public interface IClassificationService
    {


        public Task<IEnumerable<Classification>> GetAllClassifications();
        public Task<Classification> GetClassificationById(int id);
        public Task<IEnumerable<ClassificationAdvancedModel>> GetAllClassificationsWithLedgerAccountsForSabha(int sabhaId);
    }
}
