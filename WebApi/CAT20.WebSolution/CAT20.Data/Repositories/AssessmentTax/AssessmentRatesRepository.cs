using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.AssessmentTax
{
    internal class AssessmentRatesRepository : Repository<AssessmentRates>, IAssessmentRatesRepository
    {
        public AssessmentRatesRepository(DbContext context) : base(context)
        {
        }
        
        
    }
}
