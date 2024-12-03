using CAT20.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.User
{
    public interface IRuleService
    {
        Task<Rule> CreateRule(Rule rule);
        Task DeleteRule(Rule rule);
        Task<IEnumerable<Rule>> GetAllRules();

        //------ [Start - GetAllUserRuleCodes] ---
        Task<List<string>> GetAllUserRuleCodes(int userId);
        //------ [End - GetAllUserRuleCodes] -----

        //------ [Start - GetAllUserPermittedModules] ---
        Task<List<string>> GetAllUserPermittedModules(int userId);
        //------ [End - GetAllUserPermittedModules] -----

        Task<Rule> GetRuleById(int id);
        Task<bool> GetChackAccessByRuleCode(int userId, string ruleCode);


    }
}
