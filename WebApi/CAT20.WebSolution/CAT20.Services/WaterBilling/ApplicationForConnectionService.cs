using AutoMapper;
using CAT20.Core;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.WaterBilling;

namespace CAT20.Services.WaterBilling
{
    public class ApplicationForConnectionService : IApplicationForConnectionService
    {
        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;
        private readonly IMapper _mapper;

        public ApplicationForConnectionService(IWaterBillingUnitOfWork wb_unitOfWork,IMapper mapper)
        {
            _wb_unitOfWork = wb_unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationForConnection>> ApproveWaterConnectionApplication(List<string> applicationIds, int approvedby)
        {
            //var approvedApplicaiosns = await _wb_unitOfWork.ApplicationForConnections.GetAllByApplicationInfoByOfficeId(173);
            var approvedApplicaiosns = await _wb_unitOfWork.ApplicationForConnections.GetAllByApplicationIds(applicationIds);

            try
            {

                foreach (var application in approvedApplicaiosns)
                {
                    application.IsApproved = true;
                    application.ApprovedBy = approvedby;
                    application.ApprovedAt = DateTime.Now;

                    if (application.RequestedConnectionId.HasValue)
                    {
                        var wc = await _wb_unitOfWork.WaterConnections.GetByIdAsync(application.RequestedConnectionId);

                        if(wc!= null && wc.SubRoadId == wc.SubRoadId)
                        {
                            wc.PartnerId = application.PartnerId;
                            wc.BillingId = application.BillingId;
                            wc.UpdatedAt = DateTime.Now;
                            wc.UpdatedBy = approvedby;

                            var auditLog = _mapper.Map<WaterConnection, ConnectionAuditLog>(wc);

                            auditLog.WaterConnectionId = wc.Id;
                            auditLog.Action = WbAuditLogAction.ChnageRequest;
                            auditLog.ActionBy = approvedby;
                            auditLog.ActiveStatus = 1;
                            auditLog.EntityType = WbEntityType.CustomerChange;
                            auditLog.Id = 0;


                            await _wb_unitOfWork.WaterConnectionAuditLogs.AddAsync(auditLog);
                        }
                        else
                        {
                            throw new ("Water Connection not found");
                        }
                    }

                }

                await _wb_unitOfWork.CommitAsync();

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message.ToString()); 
                return approvedApplicaiosns.DefaultIfEmpty();
            }

            return approvedApplicaiosns;
        }

        public async Task<ApplicationForConnection> Create(ApplicationForConnection obj, int OfficeId)
        {


            try
            {

                var numberSequence = await _wb_unitOfWork.NumberSequences.GetNumberByOfficeId(OfficeId);
                if (numberSequence == null)
                {
                    var newEntity = new NumberSequence
                    {
                        Id = null,
                        OfficeId = OfficeId,
                        CoreNumber = 1,
                        ApplicationNumber = 1,
                        Status = 1,
                    };


                    await _wb_unitOfWork.NumberSequences.AddAsync(newEntity);
                    numberSequence = newEntity;
                }
                else
                {
                    numberSequence.ApplicationNumber++;
                    await _wb_unitOfWork.NumberSequences.UpdateApplicationNumberAsync(numberSequence);
                }

                var generateApplicationNo = OfficeId.ToString() + '-' + numberSequence.ApplicationNumber.ToString().PadLeft(5, '0');

                //add Primary Key
                obj.ApplicationNo = generateApplicationNo;
                obj.ApprovedBy = null;
                obj.IsApproved = null;

                await _wb_unitOfWork.ApplicationForConnections.AddAsync(obj);
                await _wb_unitOfWork.CommitAsync();

                return obj;

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message.ToString());

                obj.ApplicationNo = null; return obj;

            }
        }

        public async Task Delete(ApplicationForConnection obj)
        {
            _wb_unitOfWork.ApplicationForConnections.Remove(obj);
            await _wb_unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ApplicationForConnection>> GetAllApplicationsForOffice(int officeId)
        {
            return await _wb_unitOfWork.ApplicationForConnections.GetAllApplicationsForOffice(officeId);
        }

        public async Task<IEnumerable<ApplicationForConnection>> GetAllApplicationsForSabha(List<int> officeIdListOfSabha)
        {
            return await _wb_unitOfWork.ApplicationForConnections.GetAllApplicationsForSabha(officeIdListOfSabha);
        }

        public async Task<IEnumerable<ApplicationForConnection>> GetAllApplicationWaterConnections()
        {
            return await _wb_unitOfWork.ApplicationForConnections.GetAllAsync();
        }

        public async Task<IEnumerable<ApplicationForConnection>> GetAllNewlyApprovedApplicationsForOffice(int officeId)
        {
            return await _wb_unitOfWork.ApplicationForConnections.GetAllNewlyApprovedApplicationsForOffice(officeId);
        }

        public async Task<IEnumerable<ApplicationForConnection>> GetAllNewlyApprovedApplicationsForSabha(List<int> officeIdListOfSabha)
        {
            return await _wb_unitOfWork.ApplicationForConnections.GetAllNewlyApprovedApplicationsForSabha(officeIdListOfSabha);
        }

        public async Task<IEnumerable<ApplicationForConnection>> GetAllPendingApprovalApplicationsForSabha(List<int> officeIdListOfSabha)
        {
            return await _wb_unitOfWork.ApplicationForConnections.GetAllPendingApprovalApplicationsForSabha(officeIdListOfSabha);
        }

        public async Task<ApplicationForConnection> GetApprovedApplicationInfoById(string applicationNo)
        {
            return await _wb_unitOfWork.ApplicationForConnections.GetApprovedApplicationInfoById(applicationNo);
        }

        public async Task<ApplicationForConnection> GetById(string applicationNo)
        {
            return await _wb_unitOfWork.ApplicationForConnections.GetById(applicationNo);
        }

        public async Task<ApplicationForConnection> GetInfoById(string applicationNo)
        {
            return await _wb_unitOfWork.ApplicationForConnections.GetInfoById(applicationNo);
        }

        public async Task<ApplicationForConnection> IsDeleteble(string applicationNo)
        {
            return await _wb_unitOfWork.ApplicationForConnections.IsDeleteble(applicationNo);
        }

        public async Task RejectWaterConnectionApplication(ApplicationForConnection objToBeUpdated, ApplicationForConnection obj)
        {

            objToBeUpdated.IsApproved = false;
            objToBeUpdated.ApprovedBy = obj.UpdatedBy;
            objToBeUpdated.Comment = obj.Comment;
            await _wb_unitOfWork.CommitAsync();

        }

        public async Task<bool> Update(ApplicationForConnection objToBeUpdated, ApplicationForConnection obj)
        {
            try
            {
                objToBeUpdated.BillingId = obj.BillingId;
                objToBeUpdated.SubRoadId = obj.SubRoadId;
                objToBeUpdated.RequestedNatureId = obj.RequestedNatureId;
                objToBeUpdated.IsApproved = null;
                objToBeUpdated.ApprovedBy = null;
                objToBeUpdated.UpdatedBy = obj.UpdatedBy;
                objToBeUpdated.UpdatedAt = DateTime.Now;

                await _wb_unitOfWork.CommitAsync();
                return true;
            }
            catch(Exception ex) {

                return false;

            }
        }


        //--------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<ApplicationForConnection>> GetAllRejectedApplicationsForOffice(int officeId)
        {
            return await _wb_unitOfWork.ApplicationForConnections.GetAllRejectedApplicationsForOffice(officeId);
        }

        public async Task<IEnumerable<ApplicationForConnection>> GetAllRejectedApplicationsForSabha(List<int> officeIdListOfSabha)
        {
            return await _wb_unitOfWork.ApplicationForConnections.GetAllRejectedApplicationsForSabha(officeIdListOfSabha);
        }

        //----- [Start - getAllApprovedApplicationsForOffice/{office id}] ---------------
        public async Task<IEnumerable<ApplicationForConnection>> GetAllApprovedApplicationsForOffice(int officeId)
        {
            return await _wb_unitOfWork.ApplicationForConnections.GetAllApprovedApplicationsForOffice(officeId);
        }
        //----- [End - getAllApprovedApplicationsForOffice/{office id}] -----------------


        //----- [Start - getAllApprovedApplicationForSabha/{sabha Id}] -------------
        public async Task<IEnumerable<ApplicationForConnection>> GetAllApprovedApplicationsForSabha(List<int> officeIdListOfSabha)
        {
            return await _wb_unitOfWork.ApplicationForConnections.GetAllApprovedApplicationsForSabha(officeIdListOfSabha);
        }

        public async Task<IEnumerable<ApplicationForConnection>> GetAllPendingCustomerChangeRequest(List<int> officeIdListOfSabha)
        {
            return await _wb_unitOfWork.ApplicationForConnections.GetAllPendingCustomerChangeRequest(officeIdListOfSabha);
        }
        //----- [End - getAllApprovedApplicationForSabha/{sabha Id}] ---------------
        //--------------------------------------------------------------------------------------------------
    }
}
