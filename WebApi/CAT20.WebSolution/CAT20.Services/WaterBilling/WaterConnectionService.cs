using CAT20.Core;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.WaterBilling;

namespace CAT20.Services.WaterBilling
{
    public class WaterConnectionService : IWaterConnectionService
    {
        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;
    

        public WaterConnectionService(IWaterBillingUnitOfWork wb_unitOfWork)
        {
            _wb_unitOfWork = wb_unitOfWork;
        }



        public async Task<WaterConnection> Create(string applicationNo, string meterConnectionInfoId, int officeId, DateOnly InstallDate, int CreatedBy)
        {
            try
            {

                var mci = await _wb_unitOfWork.MeterConnectInfos.GetById(meterConnectionInfoId);

                mci.IsAssigned = true;
                mci.UpdatedAt = DateTime.Now;

                var application = await _wb_unitOfWork.ApplicationForConnections.GetApprovedApplicationInfoById(applicationNo);
                var newwaterConnection = new WaterConnection
                {
                    Id = null,
                    PartnerId = application.PartnerId,
                    BillingId = application.BillingId,
                    SubRoadId = application.SubRoadId,
                    InstallDate = InstallDate,
                    ConnectionId = mci.ConnectionId,
                    ActiveNatureId = application.RequestedNatureId,
                    OfficeId = officeId,
                    /*
                     * connected = 1
                     * disconnected =2
                     * temporary block =3
                     */
                    ActiveStatus = 1,
                    NatureChangeRequest = false,
                    StatusChangeRequest = false,
                    Status = 1,
                    CreatedBy = CreatedBy,
                };

                application.IsAssigned = true;
                await _wb_unitOfWork.WaterConnections.AddAsync(newwaterConnection);
                await _wb_unitOfWork.CommitAsync();

                foreach (var submitedDocument in application.SubmittedDocuments)
                {
                    var wcdoc = new WaterBillDocument
                    {
                        Id = null,
                        ConnectionId = newwaterConnection.Id,
                        DocType = submitedDocument.DocType,
                        Uri = submitedDocument.Uri,
                        Status = 1,
                        CreatedBy = submitedDocument.CreatedBy

                    };
                    await _wb_unitOfWork.WaterBillDocuments.AddAsync(wcdoc);

                }

                var nlog = new WaterConnectionNatureLog
                {
                    Id = null,
                    NatureId = application.RequestedNatureId,
                    ConnectionId = newwaterConnection.Id,
                    ActionBy = application.ApprovedBy,
                    Status = 1,
                    CreatedBy = CreatedBy,
                    ActivatedDate = InstallDate.ToDateTime(TimeOnly.Parse("00:01 AM")),


                };

                var slog = new WaterConnectionStatusLog
                {
                    Id = null,
                    ConnectionId = newwaterConnection.Id,
                    ConnectionStatus = 1,
                    Comment = "system Init",
                    ActionBy = application.ApprovedBy,
                    Status = 1,
                    CreatedBy = CreatedBy,
                    ActivatedDate = InstallDate.ToDateTime(TimeOnly.Parse("00:01 AM")),

                };


                await _wb_unitOfWork.WaterConnectionNatureLogs.AddAsync(nlog);
                await _wb_unitOfWork.WaterConnectionStatusLogs.AddAsync(slog);


                


                var auditLog = new ConnectionAuditLog
                {

                    WaterConnectionId = newwaterConnection.Id,
                    Action = WbAuditLogAction.Create,
                    ActionBy = CreatedBy,
                    EntityType = WbEntityType.WaterConnection,

                };

                await _wb_unitOfWork.WaterConnectionAuditLogs.AddAsync(auditLog);

                await _wb_unitOfWork.CommitAsync();

                return newwaterConnection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new WaterConnection();
            }

        }

        public Task Delete(WaterConnection obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WaterConnection>> GetAllConnectionByPartner(int officeid, int partnerId)
        {
            return await _wb_unitOfWork.WaterConnections.GetAllConnectionByPartner(officeid, partnerId);
        }

        public async Task<IEnumerable<WaterConnection>> GetAllConnectionsWithZeroOpenBalanceForOffice(int officeId)
        {
            return await _wb_unitOfWork.WaterConnections.GetAllConnectionsWithZeroOpenBalanceForOffice(officeId);
        }

        public async Task<IEnumerable<WaterConnection>> getAllConnNatureChangeRequestForSabha(List<int> officeIdListOfSabha)
        {
            return await _wb_unitOfWork.WaterConnections.getAllConnNatureChangeRequestForSabha(officeIdListOfSabha);
        }

        public async Task<IEnumerable<WaterConnection>> getAllConnStatusChangeRequestForSabha(List<int> officeIdListOfSabha)
        {
            return await _wb_unitOfWork.WaterConnections.getAllConnStatusChangeRequestForSabha(officeIdListOfSabha);
        }


        public async Task<IEnumerable<WaterConnection>> GetAllWaterConnections()
        {
            return await _wb_unitOfWork.WaterConnections.GetAllAsync();
        }

        public async Task<IEnumerable<WaterConnection>> GetAllWaterConnectionsForOffice(int officeId)
        {
            return await _wb_unitOfWork.WaterConnections.GetAllWaterConnectionsForOffice(officeId);
        }

        public async Task<(int totalCount, IEnumerable<WaterConnection> list)> GetAllWaterConnectionsForSabha(List<int> officeIdListOfSabha, int? waterProjectId, int? subRoadId, int? natureId, int? connectionStatus, int pageNo, int pageSize, string? filterValue)
        {
            return await _wb_unitOfWork.WaterConnections.GetAllWaterConnectionsForSabha(officeIdListOfSabha,waterProjectId,subRoadId,natureId,connectionStatus,pageNo,pageSize,filterValue);
        }
        public async Task<(int totalCount, IEnumerable<WaterConnection> list)> getAllMeterReadingUpdatedWaterConnectionsForSabhaAndMonth(List<int> officeIdListOfSabha, int? waterProjectId, int? subRoadId, int? natureId, int? connectionStatus, int pageNo, int pageSize, string? filterValue, int year, int month)
        {
            return await _wb_unitOfWork.WaterConnections.getAllMeterReadingUpdatedWaterConnectionsForSabhaAndMonth(officeIdListOfSabha, waterProjectId, subRoadId, natureId, connectionStatus, pageNo, pageSize, filterValue, year, month);
        }
        public async Task<(int totalCount, IEnumerable<WaterConnection> list)> getAllMeterReadingNotUpdatedWaterConnectionsForSabhaAndMonth(List<int> officeIdListOfSabha, int? waterProjectId, int? subRoadId, int? natureId, int? connectionStatus, int pageNo, int pageSize, string? filterValue, int? year, int? month)
        {
            return await _wb_unitOfWork.WaterConnections.getAllMeterReadingNotUpdatedWaterConnectionsForSabhaAndMonth(officeIdListOfSabha, waterProjectId, subRoadId, natureId, connectionStatus, pageNo, pageSize, filterValue, year, month);
        }

        public async Task<WaterConnection> GetById(int id)
        {
            return await _wb_unitOfWork.WaterConnections.GetByIdAsync(id);
        }

        public async Task<WaterConnection> GetInfoById(int id)
        {
            return await _wb_unitOfWork.WaterConnections.GetInfoById(id);
        }

        public Task<IEnumerable<WaterConnection>> GetWaterConnections()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WaterConnection>> SearchWaterConnectionsByConnectionId(string searchQuery)
        {
            return await _wb_unitOfWork.WaterConnections.SearchWaterConnectionsByConnectionId(searchQuery);
        }

        public async Task Update(WaterConnection objToBeUpdated, WaterConnection obj)
        {
            objToBeUpdated.ConnectionId = obj.ConnectionId;
            objToBeUpdated.InstallDate = obj.InstallDate;


            objToBeUpdated.UpdatedBy = obj.UpdatedBy;
            objToBeUpdated.UpdatedAt = DateTime.Now;

            await _wb_unitOfWork.CommitAsync();
        }

        //--------------- [Start - GetAllWaterConnectionForSubRoad(int subRoadId)] -----
        public async Task<IEnumerable<WaterConnection>> GetAllWaterConnectionForSubRoad(int subRoadId)
        {
            return await _wb_unitOfWork.WaterConnections.GetAllWaterConnectionForSubRoad(subRoadId);
        }
        //--------------- [End - GetAllWaterConnectionForSubRoad(int subRoadId)] -------


        //--------------- [Start - GetAllNotAddOpenBalanceWaterConnectionForSubRoad(int subRoadId)] -------
        public async Task<IEnumerable<WaterConnection>> GetAllNotAddOpenBalanceWaterConnectionForSubRoad(int subRoadId)
        {
            return await _wb_unitOfWork.WaterConnections.GetAllNotAddOpenBalanceWaterConnectionForSubRoad(subRoadId);
        }
    
        public Task<int> NumberOfConnectionForSubRoad(int subRoadId)
        {
            return _wb_unitOfWork.WaterConnections.NumberOfConnectionForSubRoad(subRoadId);
        }

        public async Task<IEnumerable<WaterConnection>> getWaterConnectionsBySubRoad(int subRoadId)
        {
            return await _wb_unitOfWork.WaterConnections.getWaterConnectionsBySubRoad(subRoadId);
        }

        public async Task<IEnumerable<WaterConnection>> GetWaterConnectionsForSabhaAndPartnerId(int sabhaId, int partnerId)
        {
            return await _wb_unitOfWork.WaterConnections.GetWaterConnectionsForSabhaAndPartnerId(sabhaId, partnerId);
        }
    }
}
