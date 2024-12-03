using CAT20.Core.Models.HRM.PersonalFile;
using CAT20.Core;
using CAT20.Core.Services.HRM.PersonalFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.HRM.PersonalFile
{
    public class ServiceTypeDataService : IServiceTypeDataService
    {
        private readonly IHRMUnitOfWork _unitOfWork;
        private readonly IAuditTrailUnitOfWork _auditTrailUnitOfWork;
        public ServiceTypeDataService(IHRMUnitOfWork unitOfWork, IAuditTrailUnitOfWork auditTrailUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _auditTrailUnitOfWork = auditTrailUnitOfWork;
        }

        public async Task<ServiceTypeData> GetServiceTypeDataById(int id)
        {
            return await _unitOfWork.ServiceTypeDatas.GetServiceTypeDataById(id);
        }
        public async Task<IEnumerable<ServiceTypeData>> GetServiceTypeDataBySalaryStructureId(int id)
        {
            return await _unitOfWork.ServiceTypeDatas.GetServiceTypeDataBySalaryStructureId(id);
        }

        public async Task<IEnumerable<ServiceTypeData>> GetAllServiceTypeData()
        {
            return await _unitOfWork.ServiceTypeDatas.GetAllServiceTypeData();
        }
    }
}
