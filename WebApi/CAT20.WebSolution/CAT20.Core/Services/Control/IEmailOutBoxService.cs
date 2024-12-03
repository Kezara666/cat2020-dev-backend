using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IEmailOutBoxService
    {
        Task<IEnumerable<EmailOutBox>> GetAllEmails();
        Task<EmailOutBox> GetEmailOutBoxById(int id);
        Task<EmailOutBox> CreateEmailOutBox(EmailOutBox emailOutBox);
        Task UpdateEmailOutBox(EmailOutBox emailOutBoxNew, EmailOutBox emailOutBoxDB);
        Task DeleteEmailOutBox(EmailOutBox emailOutBox);
        Task<IEnumerable<EmailOutBox>> GetAllPendingAsync();
    }
}
