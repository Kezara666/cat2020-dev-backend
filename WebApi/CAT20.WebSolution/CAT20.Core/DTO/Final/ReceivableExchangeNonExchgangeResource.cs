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

    public class ReceivableExchangeNonExchgangeResource
    {
        public int? Id { get; set; }
        public int? ExchangeType { get; set; }
        public int? LedgerAccountId { get; set; }
        public VoucherPayeeCategory? ReceivableCategory { get; set; }

        public int? ReceivableFromId { get; set; }
        public int? FinancialYear { get; set; }
        public decimal Amount { get; set; }

        ///*mandatory filed*/

        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


        public virtual CreditorDebtorResource? CreditorDebtorInfo { get; set; }

        public virtual VoteDetailLimitedresource? LedgerVoteDetail { get; set; }
    }
}