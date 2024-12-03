using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;
using CAT20.Core.Models.Enums;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveVoucherLineResource
    {

        public int? Id { get; set; }
        public int VoucherId { get; set; }

        public string? CommentOrDescription { get; set; }
        public int? CommitmentLineId { get; set; }
        public int? CommitmentLineVoteId { get; set; }
        public int? DepositLineId { get; set; }
        public int? SubImprestLineId { get; set; }
        public int VoteId { get; set; }
        public int VoteAllocationId { get; set; }

        public string VoteCode { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal VAT { get; set; }
        public decimal NBT { get; set; }
        //public PaymentStatus PaymentStatus { get; set; }

        public virtual ICollection<SaveVoucherSubLineResources>? VoucherSubLines { get; set; }

    }
}
