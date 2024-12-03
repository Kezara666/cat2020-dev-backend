using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.Models.FinalAccount
{
        /*
         * water billing 
         * electricity billing
         * telephone billing
         * for sabha
         *
         */
    public  class CreditorBilling
    {
        [Key]
        public int? Id {  get; set; }
        [Required]
        public int? LedgerAccountId {  get; set; }

        [Required]
        public int? CustomVoteId { get; set; }

        [Required]
        public string? Description { get; set; }
        [Required]
        public int? CreditorId { get; set; }
        [Required]
        public VoucherPayeeCategory? CreditorCategory { get; set; }
        [Required]
        public int? Year { get; set; }
        [Required]
        public int? Month { get; set; }

        [Precision(18,2)]
        public decimal Amount { get; set; }

        /*mandatory filed*/

        [Required]
        public int? OfficeId { get; set; }
        [Required]
        public int? SabhaId { get; set; } 
        public int Status { get; set; }
        [Required]
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? SystemActionAt { get; set; }
    }
}
