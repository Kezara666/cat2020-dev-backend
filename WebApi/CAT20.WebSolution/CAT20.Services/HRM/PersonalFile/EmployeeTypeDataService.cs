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
    public class EmployeeTypeDataService : IEmployeeTypeDataService
    {
        private readonly IHRMUnitOfWork _unitOfWork;
        private readonly IAuditTrailUnitOfWork _auditTrailUnitOfWork;
        public EmployeeTypeDataService(IHRMUnitOfWork unitOfWork, IAuditTrailUnitOfWork auditTrailUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _auditTrailUnitOfWork = auditTrailUnitOfWork;
        }

        public async Task<EmployeeTypeData> GetEmployeeTypeDataById(int id)
        {
            return await _unitOfWork.EmployeeTypeDatas.GetEmployeeTypeDataById(id);
        }

        public async Task<IEnumerable<EmployeeTypeData>> GetAllEmployeeTypeData()
        {
            return await _unitOfWork.EmployeeTypeDatas.GetAllEmployeeTypeData();
        }
    }
}
