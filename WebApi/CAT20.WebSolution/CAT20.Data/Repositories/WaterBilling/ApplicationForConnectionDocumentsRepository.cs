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
    public class ApplicationForConnectionDocumentsRepository : Repository<ApplicationForConnectionDocument>, IApplicationForConnectionDocumentsRepository
    {
        public ApplicationForConnectionDocumentsRepository(DbContext context) : base(context)
        {
            
        }

        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }

        public async Task<IEnumerable<ApplicationForConnectionDocument>> GetAllDocumentsForApplication(string applicationNo)
        {
            return await waterBillingDbContext.ApplicationForConnectionDocuments.Where(afcd => afcd.ApplicationNo == applicationNo).ToListAsync();
        }
    }
}
