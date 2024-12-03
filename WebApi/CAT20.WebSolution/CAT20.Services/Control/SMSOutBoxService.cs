using CAT20.Core;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.Control
{
    public class SMSOutBoxService : ISMSOutBoxService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public SMSOutBoxService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SMSOutBox> CreateSMSOutBox(SMSOutBox smsOutBox)
        {
            if (smsOutBox.ID == 0)
                await _unitOfWork.SMSOutBoxes
                    .AddAsync(smsOutBox);
            await _unitOfWork.CommitAsync();

            return smsOutBox;
        }

        public async Task DeleteSMSOutBox(SMSOutBox smsOutBox)
        {
            _unitOfWork.SMSOutBoxes.Remove(smsOutBox);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<SMSOutBox>> GetAllSMSs()
        {
            return await _unitOfWork.SMSOutBoxes.GetAllAsync();
        }

        public async Task<SMSOutBox> GetSMSOutBoxById(int id)
        {
            return await _unitOfWork.SMSOutBoxes.GetByIdAsync(id);
        }

        public async Task<SMSOutBox> GetSMSOutBoxBySabhaId(int sabhaid)
        {
            return await _unitOfWork.SMSOutBoxes.GetByIdAsync(sabhaid);
        }

        public async Task<IEnumerable<SMSOutBox>> GetAllPendingAsync()
        {
            return await _unitOfWork.SMSOutBoxes.GetAllPendingAsync();
        }

        public async Task UpdateSMSOutBox(SMSOutBox smsOutBoxNew, SMSOutBox smsOutBoxDB)
        {
            await _unitOfWork.CommitAsync();
        }
    }
}
