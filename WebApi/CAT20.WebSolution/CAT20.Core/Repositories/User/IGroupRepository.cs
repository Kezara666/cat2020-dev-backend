using CAT20.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.User
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<List<Group>> GetAllGroupsForSabhaId(int SabhaId);
        Task<Group> GetByIdAsync(int id); 
        Task<Group> GetRIGroupForSabhaAsync(int sabhaid);
        Task<Group> GetMeterReaderGroupForSabhaAsync(int sabhaid);
        Task<List<GroupRule>> GetAllForGroupRules(Group groupObj);
        Task<List<GroupUser>> GetAllForGroupUsers(Group groupObj);
    }
}
