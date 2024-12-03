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

    public class FixedDepositResource
    {

        public int? Id { get; set; }
        public int? DepositTypeVote { get; set; }
        public int? BankBranchId { get; set; }
        public string? Reference { get; set; }

        public decimal? InterestRate { get; set; }

        public decimal? FDAmount { get; set; }

        public DateTime DepositDate { get; set; }

        public int? Duration { get; set; }
        public DateTime RenewableDate { get; set; }

        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


        public virtual FinalBankBranchResource? BankBranch { get; set; }

        public virtual VoteDetailLimitedresource? DepositTypeVoteDetail { get; set; }
    }
}