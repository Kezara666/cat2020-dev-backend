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
    public class EmailConfigurationService : IEmailConfigurationService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public EmailConfigurationService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmailConfiguration> GetEmailConfigurationById(int id)
        {
            return await _unitOfWork.EmailConfigurations.GetByIdAsync(id);
        }
    }
}
