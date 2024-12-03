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
    public class ClassLevelDataService : IClassLevelDataService
    {
        private readonly IHRMUnitOfWork _unitOfWork;
        private readonly IAuditTrailUnitOfWork _auditTrailUnitOfWork;
        public ClassLevelDataService(IHRMUnitOfWork unitOfWork, IAuditTrailUnitOfWork auditTrailUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _auditTrailUnitOfWork = auditTrailUnitOfWork;
        }

        public async Task<ClassLevelData> GetClassLevelDataById(int id)
        {
            return await _unitOfWork.ClassLevelDatas.GetClassLevelDataById(id);
        }

        public async Task<IEnumerable<ClassLevelData>> GetAllClassLevelData()
        {
            return await _unitOfWork.ClassLevelDatas.GetAllClassLevelData();
        }
    }
}
