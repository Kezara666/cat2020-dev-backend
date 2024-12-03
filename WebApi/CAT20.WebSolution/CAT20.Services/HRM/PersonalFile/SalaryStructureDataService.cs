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
    public class SalaryStructureDataService : ISalaryStructureDataService
    {
        private readonly IHRMUnitOfWork _unitOfWork;
        private readonly IAuditTrailUnitOfWork _auditTrailUnitOfWork;
        public SalaryStructureDataService(IHRMUnitOfWork unitOfWork, IAuditTrailUnitOfWork auditTrailUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _auditTrailUnitOfWork = auditTrailUnitOfWork;
        }

        public async Task<SalaryStructureData> GetSalaryStructureDataById(int id)
        {
            return await _unitOfWork.SalaryStructureDatas.GetSalaryStructureDataById(id);
        }

        public async Task<IEnumerable<SalaryStructureData>> GetAllSalaryStructureData()
        {
            return await _unitOfWork.SalaryStructureDatas.GetAllSalaryStructureData();
        }
    }
}
