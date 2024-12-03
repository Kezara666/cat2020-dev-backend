using CAT20.Core.Models.User;
using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CAT20.Core.Services.User;

namespace CAT20.Services.User
{
    public class GroupRuleService: IGroupRuleService
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public GroupRuleService(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SaveRules(List<GroupRule> newList, List<GroupRule> deleteList)
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
                        await _unitOfWork.GroupRules.AddAsync(item);
                    }

                    foreach (var item in deleteList)
                    {
                        _unitOfWork.GroupRules.Remove(item);
                    }
                    scope.Complete();
                }
                catch (Exception)
                {
                    scope.Dispose();
                }
            }
        }


        public async Task<GroupRule> CreateGroupRule(GroupRule groupRule)
        {
            try
            {
                await _unitOfWork.GroupRules.AddAsync(groupRule);

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
            catch (Exception)
            {
            }
            return groupRule;
        }

        public async Task DeleteGroupRule(GroupRule groupRule)
        {
            try
            {
                _unitOfWork.GroupRules.Remove(groupRule);

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
        public async Task<List<GroupRule>> GetAllForAsync(Group group)
        {
            return await _unitOfWork.GroupRules.GetAllforAsync(group);
        }
    }
}
