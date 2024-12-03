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
    public class QH3Repository : Repository<QH3>, IQH3Repository
    {
        public QH3Repository(DbContext context) : base(context)
        {
        }
    }
}
