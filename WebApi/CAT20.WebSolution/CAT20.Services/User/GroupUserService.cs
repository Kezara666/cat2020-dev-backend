using CAT20.Core;
using CAT20.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Transactions;
using CAT20.Core.Services.User;

namespace CAT20.Services.User
{
    public class GroupUserService : IGroupUserService
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public GroupUserService(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SaveUsers(List<GroupUser> newList, List<GroupUser> deleteList)
        {
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                try
                {
                    foreach (var item in newList)
                    {
                        await _unitOfWork.GroupUsers.AddAsync(item);
                    }

                    //foreach (var item in deleteList)
                    //{
                    //    _unitOfWork.GroupUsers.Remove(item);
                    //}
                    await _unitOfWork.CommitAsync();
                    scope.Complete();

                }

                catch (Exception ex)
                {
                    Console.Write(ex);
                    var m = ex.Message;
                    scope.Dispose();
                }
            }
        }

        public async Task<GroupUser> CreateGroupUser(GroupUser groupUser)
        {
            try
            {
                await _unitOfWork.GroupUsers.AddAsync(groupUser);

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
                var exception = ex;
            }
            return groupUser;
        }

        public async Task DeleteGroupUser(GroupUser groupUser)
        {
            try
            {
                _unitOfWork.GroupUsers.Remove(groupUser);

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

        public async Task<List<GroupUser>> GetAllForAsync(Group group)
        {
            try
            {
                return await _unitOfWork.GroupUsers.GetAllforAsync(group);
            }
            catch (Exception ex)
            {
                var exception = ex;
                return null;
            }
        }
    }
}
