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
    public class SupportingDocTypeDataService : ISupportingDocTypeDataService
    {
        private readonly IHRMUnitOfWork _unitOfWork;
        private readonly IAuditTrailUnitOfWork _auditTrailUnitOfWork;
        public SupportingDocTypeDataService(IHRMUnitOfWork unitOfWork, IAuditTrailUnitOfWork auditTrailUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _auditTrailUnitOfWork = auditTrailUnitOfWork;
        }

        public async Task<SupportingDocTypeData> GetSupportingDocTypeDataById(int id)
        {
            return await _unitOfWork.SupportingDocTypeDatas.GetSupportingDocTypeDataById(id);
        }

        public async Task<IEnumerable<SupportingDocTypeData>> GetAllSupportingDocTypeData()
        {
            return await _unitOfWork.SupportingDocTypeDatas.GetAllSupportingDocTypeData();
        }
    }
}
