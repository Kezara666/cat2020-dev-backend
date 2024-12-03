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
    public class AgentsRepository : Repository<Agents> , IAgentsRepository
    {
        public AgentsRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Agents>> GetAgentsForSabha(int sabhaId)
        {
            return await controlDbContext.Agents
                .Include(a => a.BankBranch)
                .Include(a => a.BankBranch!.Bank)
                .Where(a => a.SabhaId == sabhaId && a.Status==1).ToListAsync();
        }

        public async Task<Agents> GetAgentByIdWithBankInfo(int agentId)
        {
            return await controlDbContext.Agents
                .Include(a => a.BankBranch)
                .Include(a => a.BankBranch!.Bank)
                .FirstOrDefaultAsync(a => a.Id == agentId);
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }



    }
}
