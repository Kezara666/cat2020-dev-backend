﻿using CAT20.Core.Models.HRM.PersonalFile;
using CAT20.Core;
using CAT20.Core.Services.HRM.PersonalFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.HRM.PersonalFile
{
    public class JobTitleDataService : IJobTitleDataService
    {
        private readonly IHRMUnitOfWork _unitOfWork;
        private readonly IAuditTrailUnitOfWork _auditTrailUnitOfWork;
        public JobTitleDataService(IHRMUnitOfWork unitOfWork, IAuditTrailUnitOfWork auditTrailUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _auditTrailUnitOfWork = auditTrailUnitOfWork;
        }

        public async Task<JobTitleData> GetJobTitleDataById(int id)
        {
            return await _unitOfWork.JobTitleDatas.GetJobTitleDataById(id);
        }
        public async Task<IEnumerable<JobTitleData>> GetAllJobTitleDataByServiceTypeDataId(int id)
        {
            return await _unitOfWork.JobTitleDatas.GetAllJobTitleDataByServiceTypeDataId(id);
        }
        public async Task<IEnumerable<JobTitleData>> GetAllJobTitleData()
        {
            return await _unitOfWork.JobTitleDatas.GetAllJobTitleData();
        }
    }
}
