using CAT20.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.User
{
    public interface IGroupUserService
    {
        Task SaveUsers(List<GroupUser> newList, List<GroupUser> deleteList);
        Task<List<GroupUser>> GetAllForAsync(Group group);
        Task<GroupUser> CreateGroupUser(GroupUser groupUser);
        Task DeleteGroupUser(GroupUser groupUser);
    }
}
