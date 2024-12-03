using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.OtherModule;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Mixin;
using CAT20.WebApi.Resources.OnlinePayment;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.DTO.Final
{

    public class IndustrialCreditorsResource
    {

        public int? Id { get; set; }
        public int? CreditorTypeId { get; set; }
        public int? FundSourceId { get; set; }
        public int? CategoryVote { get; set; }
        public int? CreditorId { get; set; }
        public string? ProjectName { get; set; }
        public decimal Amount { get; set; }
        public VoucherPayeeCategory CreditorCategory { get; set; }

        //public virtual SabhaFundSource? FundSource { get; set; }

        public virtual IndustrialCreditorsDebtorsTypes? CreditorType { get; set; }


        /*mandatory filed*/

        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual CreditorDebtorResource? CreditorDebtorInfo { get; set; }
        public virtual VoteDetailLimitedresource? CategoryVoteDetail { get; set; }
        public virtual VoteDetailLimitedresource? FundSource { get; set; }


    }
}