using CAT20.Core.Models.AssessmentTax;
using CAT20.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public class AmalgamationAssessmentRepository : Repository<AmalgamationAssessment>, IAmalgamationAssessmentRepository
    {
        public AmalgamationAssessmentRepository(DbContext context) : base(context)
        {
        }
    }
}
