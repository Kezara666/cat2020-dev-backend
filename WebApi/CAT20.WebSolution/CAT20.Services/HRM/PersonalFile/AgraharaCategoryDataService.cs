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
    public class AgraharaCategoryDataService : IAgraharaCategoryDataService
    {
        private readonly IHRMUnitOfWork _unitOfWork;
        private readonly IAuditTrailUnitOfWork _auditTrailUnitOfWork;
        public AgraharaCategoryDataService(IHRMUnitOfWork unitOfWork, IAuditTrailUnitOfWork auditTrailUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _auditTrailUnitOfWork = auditTrailUnitOfWork;
        }

        public async Task<AgraharaCategoryData> GetAgraharaCategoryDataById(int id)
        {
            return await _unitOfWork.AgraharaCategoryDatas.GetAgraharaCategoryDataById(id);
        }

        public async Task<IEnumerable<AgraharaCategoryData>> GetAllAgraharaCategoryData()
        {
            return await _unitOfWork.AgraharaCategoryDatas.GetAllAgraharaCategoryData();
        }
    }
}
