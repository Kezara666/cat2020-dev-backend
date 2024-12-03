using CAT20.Core.Models.User;
using CAT20.Core.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.User
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(DbContext context) : base(context)
        {
        }

        private UserActivityDBContext userActivityDbContext
        {
            get { return Context as UserActivityDBContext; }
        }

        
        public async Task<Group> GetByIdAsync(int id)
        {
            return await userActivityDbContext.Groups
               .Where(m => m.ID == id)
               .FirstOrDefaultAsync();
        }

        public async Task<Group> GetRIGroupForSabhaAsync(int sabhaid)
        {
            return await userActivityDbContext.Groups
               .Where(m => m.SabhaId == sabhaid && m.Description=="RI")
               .FirstOrDefaultAsync();
        }

        public async Task<Group> GetMeterReaderGroupForSabhaAsync(int sabhaid)
        {
            var result = await userActivityDbContext.Groups
               .Where(m => m.SabhaId == sabhaid && m.Description == "MR")
               .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<GroupRule>> GetAllForGroupRules(Group groupObj)
        {
           return await userActivityDbContext.GroupRules.Include(g => g.Group).Where(t => t.GroupID == groupObj.ID).ToListAsync();
        }
        public async Task<List<GroupUser>> GetAllForGroupUsers(Group groupObj)
        {
            return await userActivityDbContext.GroupUsers.Include(g => g.Group).Where(t => t.GroupID == groupObj.ID).ToListAsync();
        }
        public async Task<List<Group>> GetAllGroupsForSabhaId(int SabhaId)
        {
            return await userActivityDbContext.Groups.Where(t => t.SabhaId == SabhaId).ToListAsync();
        }
    }
}
