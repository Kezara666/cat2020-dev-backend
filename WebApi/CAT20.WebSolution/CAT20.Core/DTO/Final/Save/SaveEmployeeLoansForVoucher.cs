using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveEmployeeLoansForVoucher
    {
        public int Id { get; set; }
        //public int? VoucherId { get; set; }
        public int? LoanId { get; set; }
        public decimal? SettleInstallmentAmount { get; set; }
        public decimal? SettleInterestAmount { get; set; }
    }
}
