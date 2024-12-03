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
    public class ReceivableExchangeNonExchange
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public int? ExchangeType { get; set; }
        [Required]
        public int? LedgerAccountId { get; set; }

        [Required]
        public int? CustomVoteId { get; set; }
        public VoucherPayeeCategory? ReceivableCategory { get; set; }

        [Required]
        public int? ReceivableFromId { get; set; }
        [Required]
        public int? FinancialYear { get; set; }
        [Precision(18, 2)]
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