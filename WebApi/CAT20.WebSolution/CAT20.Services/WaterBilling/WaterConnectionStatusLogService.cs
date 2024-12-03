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
    public class WaterConnectionStatusLogService : IWaterConnectionStatusLogService
    {

        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;

        public WaterConnectionStatusLogService(IWaterBillingUnitOfWork wb_unitOfWork)
        {
            _wb_unitOfWork = wb_unitOfWork;
        }

        public async Task<bool> Approve(WaterConnectionStatusLog objToBeUpdated, WaterConnectionStatusLog obj)
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
                wc.ActiveStatus = obj.ConnectionStatus;
                wc.StatusChangeRequest = false;

                var auditLog = new ConnectionAuditLog
                {

                    WaterConnectionId = obj.ConnectionId,
                    Action = WbAuditLogAction.Approve,
                    ActionBy = obj.UpdatedBy,
                    EntityType = WbEntityType.Status,

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

        public async Task<WaterConnectionStatusLog> Create(WaterConnectionStatusLog obj)
        {
            try
            {

                await _wb_unitOfWork.WaterConnectionStatusLogs.AddAsync(obj);

                var wc = await _wb_unitOfWork.WaterConnections.GetByIdAsync(obj.ConnectionId.Value);
                wc.StatusChangeRequest = true;


                var auditLog = new ConnectionAuditLog
                {

                    WaterConnectionId = obj.ConnectionId,
                    Action = WbAuditLogAction.ChnageRequest,
                    ActionBy = obj.UpdatedBy,
                    EntityType = WbEntityType.Status,

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

        public async Task<bool> Disapprove(WaterConnectionStatusLog objToBeUpdated, WaterConnectionStatusLog obj)
        {
            try
            {
                //_wb_unitOfWork.WaterConnectionStatusLogs.Remove(obj);

                objToBeUpdated.ActionBy = obj.ActionBy;
                objToBeUpdated.Action = WbAuditLogAction.Reject;
                objToBeUpdated.ActivatedDate = DateTime.Now;
                objToBeUpdated.ActionNote = obj.ActionNote;


                objToBeUpdated.UpdatedBy = obj.UpdatedBy;
                objToBeUpdated.UpdatedAt = DateTime.Now;

                var wc = await _wb_unitOfWork.WaterConnections.GetByIdAsync(obj.ConnectionId.Value);
                wc.StatusChangeRequest = false;




                var auditLog = new ConnectionAuditLog
                {

                    WaterConnectionId = obj.ConnectionId,
                    Action = WbAuditLogAction.Reject,
                    ActionBy = obj.UpdatedBy,
                    EntityType = WbEntityType.Status,

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

        public async Task<WaterConnectionStatusLog> GetById(int id)
        {
            return await _wb_unitOfWork.WaterConnectionStatusLogs.GetByIdAsync(id);
        }
    }
}
