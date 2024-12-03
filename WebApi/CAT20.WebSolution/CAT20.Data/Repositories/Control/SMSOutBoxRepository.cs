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
    public class SMSOutBoxRepository : Repository<SMSOutBox>, ISMSOutBoxRepository
    {
        public SMSOutBoxRepository(DbContext context) : base(context)
        {

        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }

        public async Task<IEnumerable<SMSOutBox>> GetAllPendingAsync()
        {
            return await controlDbContext.SMSOutBoxes
                .Where(t=> (t.SMSStatus != Core.Models.Enums.SMSStatus.Sent) && (t.SMSStatus != Core.Models.Enums.SMSStatus.Failed))
                .OrderByDescending(t=> t.ID)
                .ToListAsync();
        }
    }
}
