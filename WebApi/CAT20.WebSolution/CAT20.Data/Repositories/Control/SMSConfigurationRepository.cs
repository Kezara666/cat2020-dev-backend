using CAT20.Core.Models.Control;
using CAT20.Core.Repositories.Control;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Control
{
    public class SMSConfigurationRepository : Repository<SMSConfiguration>, ISMSConfigurationRepository
    {
        public SMSConfigurationRepository(DbContext context) : base(context)
        {

        }
        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }

        public async Task<SMSConfiguration> GetByIdAsync(int id)
        {
            return await controlDbContext.SMSConfigurations
                .Where(m=> m.ID == id)
                .FirstOrDefaultAsync(); 
        }

        public async Task<SMSConfiguration> GetBySabhaIdAsync(int sabhaid)
        {
            return await controlDbContext.SMSConfigurations
                .Where(m => m.SabhaId == sabhaid && m.Status==1)
                .FirstOrDefaultAsync();
        }
    }
}
