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
    public class EmailConfigurationRepository : Repository<EmailConfiguration>, IEmailConfigurationRepository
    {
        public EmailConfigurationRepository(DbContext context) : base(context)
        {

        }
        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }

        public async Task<EmailConfiguration> GetByIdAsync(int id)
        {
            return await controlDbContext.EmailConfigurations
                .Where(m=> m.ID == id)
                .FirstOrDefaultAsync(); 
        }
    }
}
