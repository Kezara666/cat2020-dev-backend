using CAT20.Core.DTO.Vote.Save;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.FInalAccount.Save
{
    public class SaveDepositResource
    {
        public int? Id { get; set; }

        public int DepositSubCategoryId { get; set; }
        public int LedgerAccountId { get; set; }
        public int? CustomVoteId { get; set; }
        public DateTime DepositDate { get; set; }
        public int SubInfoId { get; set; }
        public int? MixOrderId { get; set; }
        public String ReceiptNo { get; set; }
        public String Description { get; set; }

        public int PartnerId { get; set; }
        public decimal InitialDepositAmount { get; set; }
        public decimal ReleasedAmount { get; set; }
        //public decimal? HoldAmount { get; set; }

        //public int? SabhaId { get; set; }
        //public int? OfficeId { get; set; }


        // mandatory fields
        //public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }

     
    }
}
