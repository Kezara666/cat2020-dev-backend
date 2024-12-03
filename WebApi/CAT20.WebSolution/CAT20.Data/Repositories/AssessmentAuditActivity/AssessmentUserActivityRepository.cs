using CAT20.Core.Models.AssessmentAuditActivity;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Repositories.AssessmentAuditActivity;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.AssessmentAuditActivity
{
    public class AssessmentUserActivityRepository : Repository<AssessmentUserActivity>, IAssessmentUserActivityRepository
    {
        public AssessmentUserActivityRepository(DbContext context) : base(context)
        {
        }
    }
}
