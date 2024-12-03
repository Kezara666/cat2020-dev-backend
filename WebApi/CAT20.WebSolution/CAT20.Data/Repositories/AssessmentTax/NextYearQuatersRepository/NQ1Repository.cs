﻿using CAT20.Core.Models.AssessmentTax;
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
    public class NQ1Repository : Repository<NQ1>, INQ1Repository
    {
        public NQ1Repository(DbContext context) : base(context)
        {
        }
    }
}
