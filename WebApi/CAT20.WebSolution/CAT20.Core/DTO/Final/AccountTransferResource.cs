using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class AccountTransferResource
    {
        public int? Id { get; set; }
        public int? SabhaId { get; set; }
        public int? OfficeId { get; set; }
        public int? VoucherId { get; set; }

        public VoteTransferActions ActionState { get; set; }

        public decimal? Amount { get; set; }
        public int? FromAccountId { get; set; }
        public int? FromVoteBalanceId { get; set; }

        public int? FromVoteDetailId { get; set; }

        public int? ToAccountId { get; set; }

        public int? ToVoteBalanceId { get; set; }

        public int? ToVoteDetailId { get; set; }

        public bool? IsRefund { get; set; }
        public bool? IsFullyRefunded { get; set; }


        public decimal? RefundedAmount { get; set; }


        public string? RequestNote { get; set; }


        public ICollection<AccountTransferRefundingResource>? AccountTransferRefunding { get; set; }


        // mandatory fields
        public int? RowStatus { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        /*linking model*/

        public virtual VoucherResource? Voucher { get; set; }

        public virtual AccountDetailOnlyBankId? FromAccount { get; set; }
        public virtual AccountDetailOnlyBankId? ToAccount { get; set; }

        public VoteDetailLimitedresource? FromVoteDetail { get; set; }
        public VoteDetailLimitedresource? ToVoteDetail { get; set; }
        public bool? IsRefundable { get; set; }


    }
}
