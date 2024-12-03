using CAT20.Core.Models.Control;
using CAT20.Core;
using CAT20.Core.Services.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.Control
{
    public class SMSConfigurationService : ISMSConfigurationService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public SMSConfigurationService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SMSConfiguration> GetSMSConfigurationById(int id)
        {
            return await _unitOfWork.SMSConfigurations.GetByIdAsync(id);
        }

        public async Task<SMSConfiguration> GetSMSConfigurationBySabhaId(int sabhaid)
        {
            return await _unitOfWork.SMSConfigurations.GetBySabhaIdAsync(sabhaid);
        }
    }
}
