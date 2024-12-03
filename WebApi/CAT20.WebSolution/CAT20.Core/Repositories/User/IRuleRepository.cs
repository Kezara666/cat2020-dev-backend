using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.User;

namespace CAT20.Core.Repositories.User
{
    public interface IRuleRepository : IRepository<Rule>
    {
         Task<bool> GetChackAccessByRuleCode(int userId, string ruleCode);

        //------ [Start - GetAllUserRuleCodes] ---
        Task<List<string>> GetAllUserRuleCodes(int userId);
        //------ [End - GetAllUserRuleCodes] -----

        //------ [Start - GetAllUserPermittedModules] ---
        Task<List<string>> GetAllUserPermittedModules(int userId);
        //------ [End - GetAllUserPermittedModules] -----
    }
}
