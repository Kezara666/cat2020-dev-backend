using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.HRM.LoanManagement;
using CAT20.Core.Repositories.HRM;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.HRM.LoanManagement
{
    public class AdvancedBtypeLedgerMappingRepository : Repository<AdvanceBTypeLedgerMapping>,IAdvancedBTypeLedgerMappingRepository
    {
        public AdvancedBtypeLedgerMappingRepository(DbContext context) : base(context)
        {

        }
    
    }
}
