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
    public class SaveIndustrialCreditorsResource
    {
        public int? Id { get; set; }
        public int CreditorTypeId { get; set; }
        public int FundSourceId { get; set; }
        public int CategoryVote { get; set; }
        public int CreditorId { get; set; }
        public string ProjectName { get; set; }
        public decimal Amount { get; set; }
        public VoucherPayeeCategory CreditorCategory { get; set; }

        //public virtual SabhaFundSource? FundSource { get; set; }

        //public virtual IndustrialCreditorsDebtorsTypes? CreditorType { get; set; }


        ///*mandatory filed*/

        //public int? OfficeId { get; set; }
        //public int? SabhaId { get; set; }
        //public int Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }

        //public DateTime? SystemActionAt { get; set; }

        public int? CustomVoteId { get; set; }
    }
}
