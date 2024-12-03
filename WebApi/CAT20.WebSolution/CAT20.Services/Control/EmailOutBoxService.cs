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
    public class EmailOutBoxService : IEmailOutBoxService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public EmailOutBoxService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmailOutBox> CreateEmailOutBox(EmailOutBox emailOutBox)
        {
            if (emailOutBox.ID == 0)
                await _unitOfWork.EmailOutBoxes
                    .AddAsync(emailOutBox);
            await _unitOfWork.CommitAsync();

            return emailOutBox;
        }

        public async Task DeleteEmailOutBox(EmailOutBox emailOutBox)
        {
            _unitOfWork.EmailOutBoxes.Remove(emailOutBox);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<EmailOutBox>> GetAllEmails()
        {
            return await _unitOfWork.EmailOutBoxes.GetAllAsync();
        }

        public async Task<EmailOutBox> GetEmailOutBoxById(int id)
        {
            return await _unitOfWork.EmailOutBoxes.GetByIdAsync(id);
        }

        public async Task<IEnumerable<EmailOutBox>> GetAllPendingAsync()
        {
            return await _unitOfWork.EmailOutBoxes.GetAllPendingAsync();
        }

        public async Task UpdateEmailOutBox(EmailOutBox emailOutBoxNew, EmailOutBox emailOutBoxDB)
        {
            await _unitOfWork.CommitAsync();
        }
    }
}
