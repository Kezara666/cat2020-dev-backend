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

    public class DepositResource
    {
        public int? Id { get; set; }

        public int DepositSubCategoryId { get; set; }
        public int LedgerAccountId { get; set; }
        public int? SubInfoId { get; set; }
        public DateTime DepositDate { get; set; }
        public int? MixOrderId { get; set; }
        public int? MixOrderLineId { get; set; }
        public String ReceiptNo { get; set; }
        public String Description { get; set; }

        public int PartnerId { get; set; }
        [Precision(18, 2)]
        public decimal InitialDepositAmount { get; set; }
        [Precision(18, 2)]
        public decimal ReleasedAmount { get; set; }
        [Precision(18, 2)]
        public decimal HoldAmount { get; set; }

        public int? SabhaId { get; set; }
        public int? OfficeId { get; set; }


        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


        //liking models

        public virtual VendorResource? Partner { get; set; }
        public virtual MixinOrder? MixinOrder { get; set; }
        public virtual VoteDetailLimitedresource? VoteDetail { get; set; }

        public int? AccountId { get; set; }

        //public List<string>? CrossOrderVoteCodes { get; set; } = new List<string>();
        //public FinalUserActionByResources? UserActionBy { get; set; }

        public bool IsEditable { get; set; }
    }
}