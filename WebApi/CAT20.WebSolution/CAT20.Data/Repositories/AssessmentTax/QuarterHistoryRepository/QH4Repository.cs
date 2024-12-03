using CAT20.Core.Models.AssessmentTax.AssessmentBalanceHistory.AssessmentBalanceHistory;
using CAT20.Core.Repositories.AssessmentTax.AssessmentsBalancesHistoryRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.AssessmentTax.QuarterHistoryRepository
{
    public class QH4Repository : Repository<QH4>, IQH4Repository
    {
        public QH4Repository(DbContext context) : base(context)
        {
        }
    }
}
