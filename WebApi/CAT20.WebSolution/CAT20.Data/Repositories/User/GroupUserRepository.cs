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
    public class GroupUserRepository : Repository<GroupUser>, IGroupUserRepository
    {
        public GroupUserRepository(DbContext context) : base(context)
        {
        }

        private UserActivityDBContext userActivityDbContext
        {
            get { return Context as UserActivityDBContext; }
        }
        public async Task<List<GroupUser>> GetAllforAsync(Group group)
        {
            return await userActivityDbContext.GroupUsers.Include(g => g.Group)
                .Where(t => t.GroupID == group.ID)
                .ToListAsync();
        }
    }
}
