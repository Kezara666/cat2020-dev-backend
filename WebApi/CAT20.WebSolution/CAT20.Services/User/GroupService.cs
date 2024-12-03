using CAT20.Core;
using CAT20.Core.Models.Common;
using CAT20.Core.Models.User;
using CAT20.Core.Services.User;
using System.Globalization;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Transactions;

namespace CAT20.Services.User
{
    public class GroupService : IGroupService
    {
        private readonly IUserUnitOfWork _unitOfWork;
        private readonly IGroupUserService _groupUserService;
        public GroupService(IUserUnitOfWork unitOfWork,IGroupUserService groupUserService)
        {
            _unitOfWork = unitOfWork;
            _groupUserService = groupUserService;
        }
        public async Task<Group> CreateGroup(Group group)
        {
            try
            {
                await _unitOfWork.Groups.AddAsync(group);

                #region Audit Log
                //var note = new StringBuilder();
                //if (group.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLogUser
                //{
                //    RecordDateTime = DateTime.Now,
                //    SourceID = group.ID.Value,
                //    Transaction = Core.Models.Enums.Transaction.Group,
                //    User = new UserDetail { ID = (group.UserCreatedID ?? group.UserCreatedID.Value) },
                //    Notes = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                var error = ex;
            }
            return group;
        }
        public async Task UpdateGroup(Group groupToBeUpdate, Group group)
        {
            groupToBeUpdate.Description = group.Description;
            groupToBeUpdate.IsActive = group.IsActive;
            groupToBeUpdate.DateModified = DateTime.Now;

            #region AuditLog

            //var _sb = new StringBuilder();
            //if (currentpassword != userDetail.Password)
            //{
            //    _sb.Append("Password Changed");
            //    //_sb.Append(" by System");
            //}
            //else
            //{
            //    //_sb.Append("Updated on " + DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            //    _sb.Append("User Details Updated");
            //    //_sb.Append(" by System");
            //}

            //await _unitOfWork.AuditLogs.AddAsync(new AuditLogUser
            //{
            //    Transaction = Transaction.User,
            //    SourceID = userDetail.ID,
            //    //User = UserDetail,
            //    RecordDateTime = DateTime.Now,
            //    Notes = _sb.ToString(),
            //    UserID = userDetail.ID,
            //});

            #endregion

            await _unitOfWork.CommitAsync();

        }


        public async Task DeleteGroup(Group group)
        {
            try
            {
                _unitOfWork.Groups.Remove(group);

                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Deleted on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLogUser
                //{
                //    RecordDateTime = DateTime.Now,
                //    SourceID = group.ID.Value,
                //    Transaction = Core.Models.Enums.Transaction.Group,
                //    User = new UserDetail { ID = (group.UserCreatedID ?? group.UserCreatedID.Value) },
                //    Notes = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.Programmes.Remove(programme);
        }
        public async Task<IEnumerable<Group>> GetAllGroups()
        {
            return await _unitOfWork.Groups.GetAllAsync();
        }
        public async Task<IEnumerable<Group>> GetAllGroupsForSabhaId(int SabhaId)
        {
            return await _unitOfWork.Groups.GetAllGroupsForSabhaId(SabhaId);
        }
        public async Task<Group> GetGroupById(int id)
        {
            return await _unitOfWork.Groups.GetByIdAsync(id);
        }

        public async Task<Group> GetRIGroupForSabhaAsync(int sabhaid)
        {
            return await _unitOfWork.Groups.GetRIGroupForSabhaAsync(sabhaid);
        }

        public async Task<Group> GetMeterReaderGroupForSabhaAsync(int sabhaid)
        {
            return await _unitOfWork.Groups.GetMeterReaderGroupForSabhaAsync(sabhaid);
        }

        public async Task<List<GroupRule>> GetAllForGroupRules(Group groupObj)
        {
            return await _unitOfWork.Groups.GetAllForGroupRules(groupObj);
        }
        public async Task<List<GroupUser>> GetAllForGroupUsers(Group groupObj)
        {
            return await _unitOfWork.Groups.GetAllForGroupUsers(groupObj);
        }

        //public async Task<IEnumerable<Group>> GetAllGroupForSabhaId(int id)
        //{
        //    return await _unitOfWork.Groups.GetAllGroupForSabhaIdAsync(id);
        //}
    }
}
