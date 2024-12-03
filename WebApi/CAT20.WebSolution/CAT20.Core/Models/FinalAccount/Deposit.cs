using CAT20.Core.Models.Control;
using CAT20.Core.Models.FinalAccount.logs;
using CAT20.Core.Models.User;
using CAT20.Core.Models.Vote;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CAT20.Core.Models.FinalAccount
{
    public  class Deposit
    {
        [Key]
        public int? Id { get; set; }
        
        public int DepositSubCategoryId { get; set; }
        public int LedgerAccountId { get; set; }

        [Required]
        public int? CustomVoteId { get; set; }
        public int? SubInfoId { get; set; }
        public DateTime DepositDate { get; set; }
        public int? MixOrderId { get; set; }
        public int? MixOrderLineId { get; set; }
        public String ReceiptNo { get; set; }
        public String Description { get; set; }

        public int PartnerId {  get; set; }
        [Precision(18, 2)]
        public decimal InitialDepositAmount { get; set; }
        [Precision(18, 2)]
        public decimal ReleasedAmount { get; set; }
        [Precision(18, 2)]
        public decimal HoldAmount { get; set; }

        public int SabhaId { get; set; }
        public int OfficeId { get; set; }


        // mandatory fields
        public int? Status { get; set; }
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

        public bool IsCompleted {  get; set; }
        public bool IsEditable {  get; set; }
    }
}