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
    public class AmalgamationRepository : Repository<Amalgamation>, IAmalgamationRepository
    {
        public AmalgamationRepository(DbContext context) : base(context)
        {
        }
    }
}
