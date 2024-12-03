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
    public class EmailOutBoxRepository : Repository<EmailOutBox>, IEmailOutBoxRepository
    {
        public EmailOutBoxRepository(DbContext context) : base(context)
        {

        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }

        public async Task<IEnumerable<EmailOutBox>> GetAllPendingAsync()
        {
            return await controlDbContext.EmailOutBoxes
                .Where(t=> (t.EmailStatus != Core.Models.Enums.EmailStatus.Sent) && t.EmailStatus != Core.Models.Enums.EmailStatus.Failed)
                .OrderByDescending(t=> t.ID)
                .ToListAsync();
        }
    }
}
