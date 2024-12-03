using CAT20.Core.Models.AssessmentTax.Quarter;
using CAT20.Core.Repositories.AssessmentTax.QuarterRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.AssessmentTax.QuarterRepository
{
    public class Q2Repository : Repository<Q2>, IQ2Repository
    {
        public Q2Repository(DbContext context) : base(context)
        {
        }
    }
}
