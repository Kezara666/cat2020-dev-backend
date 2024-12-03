using CAT20.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.User
{
    public interface IGroupService
    {
        Task<Group> CreateGroup(Group group);
        Task UpdateGroup(Group groupToBeUpdate, Group group);
        Task DeleteGroup(Group group);
        Task<IEnumerable<Group>> GetAllGroups();
        Task<IEnumerable<Group>> GetAllGroupsForSabhaId(int SabhaId);
        Task<Group> GetGroupById(int id);
        Task<Group> GetRIGroupForSabhaAsync(int sabhaid);
        Task<Group> GetMeterReaderGroupForSabhaAsync(int sabhaid);
        Task<List<GroupRule>> GetAllForGroupRules(Group groupObj);
        Task<List<GroupUser>> GetAllForGroupUsers(Group groupObj);
    }
}
