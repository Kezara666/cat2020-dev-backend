using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class AccountTransfer
    {
        public int? Id { get; set; }
        public int SabhaId { get; set; }
        public int OfficeId { get; set; }
        public int VoucherId { get; set; }

        public VoteTransferActions ActionState { get; set; }

        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public int FromAccountId { get; set; }
        public int FromVoteBalanceId { get; set; }

        public int FromVoteDetailId { get; set; }

        public int ToAccountId { get; set; }

        public int ToVoteBalanceId { get; set; }

        public int ToVoteDetailId { get; set; }

        public bool IsRefund { get; set; }


        [Precision(18, 2)]
        public decimal RefundedAmount { get; set; }

        public bool? IsFullyRefunded { get; set; }


        public string? RequestNote { get; set; }


        public ICollection<AccountTransferRefunding>? AccountTransferRefunding { get; set; }


        // mandatory fields
        public int? RowStatus { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        [Required]
        public DateTime? SystemCreateAt { get; set; }

        public DateTime? SystemUpdateAt { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }


    }
}
