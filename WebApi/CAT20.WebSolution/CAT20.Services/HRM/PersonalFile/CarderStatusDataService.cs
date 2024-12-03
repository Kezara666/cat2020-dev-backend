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
    public class CarderStatusDataService : ICarderStatusDataService
    {
        private readonly IHRMUnitOfWork _unitOfWork;
        private readonly IAuditTrailUnitOfWork _auditTrailUnitOfWork;
        public CarderStatusDataService(IHRMUnitOfWork unitOfWork, IAuditTrailUnitOfWork auditTrailUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _auditTrailUnitOfWork = auditTrailUnitOfWork;
        }

        public async Task<CarderStatusData> GetCarderStatusDataById(int id)
        {
            return await _unitOfWork.CarderStatusDatas.GetCarderStatusDataById(id);
        }

        public async Task<IEnumerable<CarderStatusData>> GetAllCarderStatusData()
        {
            return await _unitOfWork.CarderStatusDatas.GetAllCarderStatusData();
        }
    }
}