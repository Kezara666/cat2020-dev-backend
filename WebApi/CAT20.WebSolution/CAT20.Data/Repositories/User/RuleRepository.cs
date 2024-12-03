using CAT20.Core.Models.User;
using CAT20.Core.Repositories.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.User
{
    public class RuleRepository : Repository<Rule>, IRuleRepository
    {
        public RuleRepository(DbContext context) : base(context)
        {
        }


        private UserActivityDBContext userActivityDbContext
        {
            get { return Context as UserActivityDBContext; }
        }

        public async Task<bool> GetChackAccessByRuleCode(int userId, string ruleCode)
        {
            var responce = false;

            var data = await userActivityDbContext.GroupUsers
                 .Include(t => t.Group)
                 .Include(t => t.User)
                 .Where(m => m.UserID == userId)
                 .FirstOrDefaultAsync();
            if (data != null)
            {
                var ruleData = await userActivityDbContext.GroupRules
                    .Include(t => t.Rule)
                    .Include(t => t.Group)
                    .Where(m => m.Rule.Code == ruleCode && m.Group.ID == data.GroupID)
                    .OrderBy(m => m.Rule.Module)
                    .FirstOrDefaultAsync();
                if (ruleData != null)
                    responce = true;
            }

            return responce;
        }

        //------ [Start - GetAllRuleCodes] ---
        /*public async Task<List<string>> GetAllRuleCodes()
        {
            var rule = await userActivityDbContext.Rules
                .Select(m => m.Code).ToListAsync();

            return rule;
        }*/

        public async Task<List<string>> GetAllUserRuleCodes(int userId)
        {
            List<string> ruleList = new List<string>();

            //Load user details | check whether user exist or not
            var userData = await userActivityDbContext.GroupUsers
                .Include(t => t.Group)
                .Include(t => t.User)
                .Where(m => m.UserID == userId)
                .FirstOrDefaultAsync();


            if (userData != null) //If user exists then add rule codes to List
            {
                //Load rule data
                ruleList = await userActivityDbContext.GroupRules
                    .Include(t => t.Rule) //Include - LEFT OUTER JOIN
                    .Include(t => t.Group) //Include - LEFT OUTER JOIN
                    .Where(x => x.GroupID == userData.GroupID)
                    .Select(m => m.Rule.Code) //Select columns from the table
                    .Distinct() //find distinct value
                    .ToListAsync();
            }
            else
            {
                //If user doesn't exist then List should be empty
                ruleList.Clear();
            }
            return ruleList;
            //------ [End - GetAllRuleCodes] -----
        }

        public async Task<List<string>> GetAllUserPermittedModules(int userId)
        {
            List<string> ruleList = new List<string>();

            //Load user details | check whether user exist or not
            var userData = await userActivityDbContext.GroupUsers
                .Include(t => t.Group)
                .Include(t => t.User)
                .Where(m => m.UserID == userId)
                .FirstOrDefaultAsync();


            if (userData != null) //If user exists then add rule codes to List
            {
                //Load rule data
                ruleList = await userActivityDbContext.GroupRules
                    .Include(t => t.Rule) //Include - LEFT OUTER JOIN
                    .Include(t => t.Group) //Include - LEFT OUTER JOIN
                    .Where(x => x.GroupID == userData.GroupID)
                    .Select(m => m.Rule.Module) //Select columns from the table
                    .Distinct() //find distinct value
                    .ToListAsync();
            }
            else
            {
                //If user doesn't exist then List should be empty
                ruleList.Clear();
            }
            return ruleList;
            //------ [End - GetAllRuleCodes] -----
        }
    }
}
