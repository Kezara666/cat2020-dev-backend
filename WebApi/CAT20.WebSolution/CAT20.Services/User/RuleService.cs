using CAT20.Core.Models.User;
using CAT20.Core.Services.User;
using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.User
{
    public class RuleService : IRuleService
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public RuleService(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Rule> CreateRule(Rule  rule)
        {
            try
            {
                await _unitOfWork.Rules
                .AddAsync(rule);

                #region Audit Log
                var note = new StringBuilder();
                if (rule.ID == 0)
                    note.Append("Created on ");
                else
                    note.Append("Edited on ");
                note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                note.Append(" by ");
                note.Append("System");


                await _unitOfWork.AuditLogs.AddAsync(new AuditLogUser
                {
                    RecordDateTime = DateTime.Now,
                    SourceID = rule.ID.Value,
                    Transaction = Core.Models.Enums.Transaction.Rule,
                    User = new UserDetail { ID = (rule.UserCreatedID ?? rule.UserCreatedID.Value) },
                    Notes = note.ToString()
                });

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }

            return rule;
        }
        public async Task DeleteRule(Rule rule)
        {
            try
            {
                _unitOfWork.Rules
             .Remove(rule);
                #region Audit Log

                var note = new StringBuilder();
                note.Append("Deleted on ");
                note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                note.Append(" by ");
                note.Append("System");

                await _unitOfWork.AuditLogs.AddAsync(new AuditLogUser
                {
                    RecordDateTime = DateTime.Now,
                    SourceID = rule.ID.Value,
                    Transaction = Core.Models.Enums.Transaction.Rule,
                    User = new UserDetail { ID = (rule.UserCreatedID ?? rule.UserCreatedID.Value) },
                    Notes = note.ToString()
                });

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.Programmes.Remove(programme);
        }
        public async Task<IEnumerable<Rule>> GetAllRules()
        {
            return await _unitOfWork.Rules.GetAllAsync();
        }

        //------ [Start - GetAllUserRuleCodes] ---
        public async Task<List<string>> GetAllUserRuleCodes(int userId)
        {
            return await _unitOfWork.Rules.GetAllUserRuleCodes(userId);
        }
        //------ [End - GetAllUserRuleCodes] -----

        //------ [Start - GetAllUserPermittedModules] ---
        public async Task<List<string>> GetAllUserPermittedModules(int userId)
        {
            return await _unitOfWork.Rules.GetAllUserPermittedModules(userId);
        }
        //------ [End - GetAllUserPermittedModules] -----

        public async Task<Rule> GetRuleById(int id)
        {
            return await _unitOfWork.Rules.GetByIdAsync(id);
        }

        public async Task<bool> GetChackAccessByRuleCode(int userId, string ruleCode)
        {
            try
            {
                return await _unitOfWork.Rules.GetChackAccessByRuleCode(userId, ruleCode);
            }
            catch (Exception ex){
                return false;
            }
        }
    }
}
