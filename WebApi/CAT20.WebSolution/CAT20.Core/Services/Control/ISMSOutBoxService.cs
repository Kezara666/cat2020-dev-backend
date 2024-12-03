using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface ISMSOutBoxService
    {
        Task<IEnumerable<SMSOutBox>> GetAllSMSs();
        Task<SMSOutBox> GetSMSOutBoxById(int id);
        Task<SMSOutBox> GetSMSOutBoxBySabhaId(int sabhaid);
        Task<SMSOutBox> CreateSMSOutBox(SMSOutBox smsOutBox);
        Task UpdateSMSOutBox(SMSOutBox smsOutBoxNew, SMSOutBox smsOutBoxDB);
        Task DeleteSMSOutBox(SMSOutBox smsOutBox);
        Task<IEnumerable<SMSOutBox>> GetAllPendingAsync();
    }
}
