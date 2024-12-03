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
    public class GroupRuleRepository : Repository<GroupRule>, IGroupRuleRepository
    {
        public GroupRuleRepository(DbContext context) : base(context)
        {
        }

        private UserActivityDBContext userActivityDbContext
        {
            get { return Context as UserActivityDBContext; }
        }
        public async Task<List<GroupRule>> GetAllforAsync(Group group)
        {
            return await userActivityDbContext.GroupRules.Include(g => g.Group)
                .Where(t => t.GroupID == group.ID)
                .ToListAsync();
        }
    }
}
