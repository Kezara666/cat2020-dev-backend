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
    public class FundingSourceDataService : IFundingSourceDataService
    {
        private readonly IHRMUnitOfWork _unitOfWork;
        private readonly IAuditTrailUnitOfWork _auditTrailUnitOfWork;
        public FundingSourceDataService(IHRMUnitOfWork unitOfWork, IAuditTrailUnitOfWork auditTrailUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _auditTrailUnitOfWork = auditTrailUnitOfWork;
        }

        public async Task<FundingSourceData> GetFundingSourceDataById(int id)
        {
            return await _unitOfWork.FundingSourceDatas.GetFundingSourceDataById(id);
        }

        public async Task<IEnumerable<FundingSourceData>> GetAllFundingSourceData()
        {
            return await _unitOfWork.FundingSourceDatas.GetAllFundingSourceData();
        }
    }
}
