using CAT20.Core;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.WaterBilling
{
    public class WaterConnectionNatureLogService : IWaterConnectionNatureLogService
    {

        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;

        public WaterConnectionNatureLogService(IWaterBillingUnitOfWork wb_unitOfWork)
        {
            _wb_unitOfWork = wb_unitOfWork;
        }

        public async Task<bool>  Approve(WaterConnectionNatureLog objToBeUpdated, WaterConnectionNatureLog obj)
        {
            try
            {
                objToBeUpdated.ActionBy = obj.ActionBy;
                objToBeUpdated.Action = WbAuditLogAction.Approve;
                objToBeUpdated.ActivatedDate = DateTime.Now;
                objToBeUpdated.ActionNote = obj.ActionNote;

                objToBeUpdated.UpdatedBy = obj.UpdatedBy;
                objToBeUpdated.UpdatedAt = DateTime.Now;

                var wc = await _wb_unitOfWork.WaterConnections.GetByIdAsync(obj.ConnectionId.Value);
                wc.ActiveNatureId = obj.NatureId;
                wc.NatureChangeRequest = false;


                var auditLog = new ConnectionAuditLog
                {

                    WaterConnectionId = obj.ConnectionId,
                    Action = WbAuditLogAction.Approve,
                    ActionBy = obj.UpdatedBy,
                    EntityType = WbEntityType.Nature,

                };

                await _wb_unitOfWork.WaterConnectionAuditLogs.AddAsync(auditLog);

                await  _wb_unitOfWork.CommitAsync();

                return true;
            }catch (Exception ex)
            {

                return false;
            }

           
        }

        public async Task<WaterConnectionNatureLog> Create(WaterConnectionNatureLog obj)
        {
            try
            {
                await _wb_unitOfWork.WaterConnectionNatureLogs.AddAsync(obj);

                var wc = await _wb_unitOfWork.WaterConnections.GetByIdAsync(obj.ConnectionId.Value);

                wc.NatureChangeRequest = true;


                var auditLog = new ConnectionAuditLog
                {

                    WaterConnectionId = obj.ConnectionId,
                    Action = WbAuditLogAction.ChnageRequest,
                    ActionBy = obj.UpdatedBy,
                    EntityType = WbEntityType.Nature,

                };

                await _wb_unitOfWork.WaterConnectionAuditLogs.AddAsync(auditLog);

                await _wb_unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return obj;
        }

        public async Task<bool> Disapprove(WaterConnectionNatureLog objToBeUpdated, WaterConnectionNatureLog obj)
        {
            try
            {
                //_wb_unitOfWork.WaterConnectionNatureLogs.Remove(obj);

                objToBeUpdated.ActionBy = obj.ActionBy;
                objToBeUpdated.Action = WbAuditLogAction.Reject;
                objToBeUpdated.ActivatedDate = DateTime.Now;
                objToBeUpdated.ActionNote = obj.ActionNote;

                objToBeUpdated.UpdatedBy = obj.UpdatedBy;
                objToBeUpdated.UpdatedAt = DateTime.Now;


                var wc = await _wb_unitOfWork.WaterConnections.GetByIdAsync(obj.ConnectionId.Value);
                wc.NatureChangeRequest = false;

                var auditLog = new ConnectionAuditLog
                {

                    WaterConnectionId = obj.ConnectionId,
                    Action = WbAuditLogAction.Reject,
                    ActionBy = obj.UpdatedBy,
                    EntityType = WbEntityType.Nature,

                };

                await _wb_unitOfWork.WaterConnectionAuditLogs.AddAsync(auditLog);

                await _wb_unitOfWork.CommitAsync();
                return true;
            }

             catch (Exception ex)
            {
            return false;

            }
        }

        public async Task<WaterConnectionNatureLog> GetById(int id)
        {
            return await _wb_unitOfWork.WaterConnectionNatureLogs.GetByIdAsync(id);
        }
    }
}
