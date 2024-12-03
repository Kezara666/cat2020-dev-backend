using CAT20.Core.DTO.Vote.Save;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveIndustrialDebtorsResource
    {
        public int? Id { get; set; }
        public int DebtorTypeId { get; set; }
        public int FundSourceId { get; set; }
        public int CategoryVote { get; set; }

        public VoucherPayeeCategory DebtorCategory { get; set; }

        public int DebtorId { get; set; }
        public string ProjectName { get; set; }
        public decimal Amount { get; set; }

        //public virtual SabhaFundSource? FundSource { get; set; }

        //public virtual IndustrialCreditorsDebtorsTypes? DebtorType { get; set; }

        ///*mandatory filed*/

        //[Required]
        //public int? OfficeId { get; set; }
        //[Required]
        //public int? SabhaId { get; set; }
        //public int Status { get; set; }
        //[Required]
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //[Required]
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }

        //public DateTime? SystemActionAt { get; set; }

        public int? CustomVoteId { get; set; }
    }
}
