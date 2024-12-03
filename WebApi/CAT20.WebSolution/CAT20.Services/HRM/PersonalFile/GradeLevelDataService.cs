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
    public class GradeLevelDataService : IGradeLevelDataService
    {
        private readonly IHRMUnitOfWork _unitOfWork;
        private readonly IAuditTrailUnitOfWork _auditTrailUnitOfWork;
        public GradeLevelDataService(IHRMUnitOfWork unitOfWork, IAuditTrailUnitOfWork auditTrailUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _auditTrailUnitOfWork = auditTrailUnitOfWork;
        }

        public async Task<GradeLevelData> GetGradeLevelDataById(int id)
        {
            return await _unitOfWork.GradeLevelDatas.GetGradeLevelDataById(id);
        }

        public async Task<IEnumerable<GradeLevelData>> GetAllGradeLevelData()
        {
            return await _unitOfWork.GradeLevelDatas.GetAllGradeLevelData();
        }
    }
}
