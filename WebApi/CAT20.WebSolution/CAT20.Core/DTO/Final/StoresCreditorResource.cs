using CAT20.Core.DTO.OtherModule;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Mixin;
using CAT20.WebApi.Resources.OnlinePayment;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.DTO.Final
{

    public class StoresCreditorResource
    {
        public int? Id { get; set; }
        public int? LedgerAccountId { get; set; }
        public VoucherPayeeCategory? SupplierCategory { get; set; }
        public int? SupplierId { get; set; }

        public string? OrderNo { get; set; }
        public DateTime? PurchasingDate { get; set; }
        public int? ReceivedNumber { get; set; }
        public string? GRN { get; set; }

        public decimal InvoiceAmount { get; set; }


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

        public virtual CreditorDebtorResource? CreditorDebtorInfo { get; set; }

        public virtual VoteDetailLimitedresource? LedgerVoteDetail { get; set; }
    }
}