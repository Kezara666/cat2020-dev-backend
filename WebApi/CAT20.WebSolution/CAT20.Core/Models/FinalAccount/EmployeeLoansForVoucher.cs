using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class EmployeeLoansForVoucher
    {
        public int Id { get; set; }
        public int? VoucherId { get; set; }
        public int? LoanId { get; set; }
        [Precision(18, 2)]
        public decimal? InstallmentAmount { get; set; }
        [Precision(18, 2)]
        public decimal? InterestAmount { get; set; }

        public virtual Voucher? Voucher { get; set; }

    }
}
