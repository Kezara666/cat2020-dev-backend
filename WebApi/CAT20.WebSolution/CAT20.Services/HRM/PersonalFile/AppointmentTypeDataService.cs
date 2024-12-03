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
    public class AppointmentTypeDataService : IAppointmentTypeDataService
    {
        private readonly IHRMUnitOfWork _unitOfWork;
        private readonly IAuditTrailUnitOfWork _auditTrailUnitOfWork;
        public AppointmentTypeDataService(IHRMUnitOfWork unitOfWork, IAuditTrailUnitOfWork auditTrailUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _auditTrailUnitOfWork = auditTrailUnitOfWork;
        }

        public async Task<AppointmentTypeData> GetAppointmentTypeDataById(int id)
        {
            return await _unitOfWork.AppointmentTypeDatas.GetAppointmentTypeDataById(id);
        }

        public async Task<IEnumerable<AppointmentTypeData>> GetAllAppointmentTypeData()
        {
            return await _unitOfWork.AppointmentTypeDatas.GetAllAppointmentTypeData();
        }
    }
}
