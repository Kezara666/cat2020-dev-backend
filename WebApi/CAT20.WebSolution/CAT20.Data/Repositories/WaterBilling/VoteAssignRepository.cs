using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.WaterBilling
{
    public class VoteAssignRepository : Repository<VoteAssign>, IVoteAssignRepository
    {
        public VoteAssignRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<VoteAssign>> GetAllForWaterProject(int waterProjectId)
        {
            return await waterBillingDbContext.VoteAssigns.Where(vas=>vas.WaterProjectId == waterProjectId).Include(vas=>vas.PaymentCategory).ToListAsync();   
        }


        public async Task<IEnumerable<VoteAssign>> GetForAllProjects(List<int> projectIds)
        {
            return await waterBillingDbContext.VoteAssigns
                .Where(vas => projectIds.Contains(vas.WaterProjectId!.Value))
                .Include(vas => vas.PaymentCategory).ToListAsync();
        }

        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }
    }
}
