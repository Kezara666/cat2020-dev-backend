using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class NewAllocationRequestRepository : Repository<NewAllocationRequest>, INewAllocationRequestRepository
    {
        public NewAllocationRequestRepository(DbContext context) : base(context)
        {
        }

        public async Task<bool> HasEntity(int assessmentId)
        {
           return await assessmentTaxDbContext.NewAllocationRequests.AnyAsync(n => n.AssessmentId == assessmentId);
        }

        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }
    }
}
