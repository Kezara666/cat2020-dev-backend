using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.Control
{
    public interface ISMSConfigurationRepository : IRepository<SMSConfiguration>
    {
        Task<SMSConfiguration> GetByIdAsync(int id);
        Task<SMSConfiguration> GetBySabhaIdAsync(int sabhaid);
    }
}
