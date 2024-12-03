﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.Models.FinalAccount
{
    public class IndustrialDebtors
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public int? DebtorTypeId { get; set; }

        [Required]
        public int? CustomVoteId { get; set; }

        [Required]
        public int? FundSourceId { get; set; }
        [Required]
        public int? CategoryVote { get; set; }
        
        public VoucherPayeeCategory DebtorCategory { get; set; }
        
        [Required]
        public int? DebtorId { get; set; }
        [Required]
        public string? ProjectName { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }

        //public virtual SabhaFundSource? FundSource { get; set; }

        public virtual IndustrialCreditorsDebtorsTypes? DebtorType { get; set; }

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