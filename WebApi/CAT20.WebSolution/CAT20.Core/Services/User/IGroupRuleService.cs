using CAT20.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.User
{
    public interface IGroupRuleService
    {
        Task SaveRules(List<GroupRule> newList, List<GroupRule> deleteList);
        Task<List<GroupRule>> GetAllForAsync(Group group);
        Task<GroupRule> CreateGroupRule(GroupRule groupRule);
        Task DeleteGroupRule(GroupRule groupRule);
    }
}
