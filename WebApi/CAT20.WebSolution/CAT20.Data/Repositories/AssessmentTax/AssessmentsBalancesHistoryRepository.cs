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
    public class AssessmentsBalancesHistoryRepository : Repository<AssessmentBalancesHistory>, IAssessmentsBalancesHistoryRepository
    {
        public AssessmentsBalancesHistoryRepository(DbContext context) : base(context)
        {
        }
    }
}
