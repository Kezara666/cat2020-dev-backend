using System;
using System.Collections.Generic;
using CAT20.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.DTO.Final
{
    public partial class FixedAssetsResource
    {

        public int? Id { get; set; }
        public int? AssetsLedgerAccountId { get; set; }
        public string? AssetsTitle { get; set; }
        public string? AssetsRegNo { get; set; }

        public FixedAssetsBalanceTypes BalanceType { get; set; }

        public DateTime? AcquiredDate { get; set; }
        public DateTime? RevalueDate { get; set; }
        public int? RemainingLifetime { get; set; }

        public int? GrantLedgerAccountId { get; set; }


        public decimal OriginalORRevaluedAmount { get; set; }
        public decimal AccumulatedDepreciation { get; set; }

        public decimal GrantAmount { get; set; }

        public decimal AccumulatedRevenueRecognition { get; set; }

        /*mandatory filed*/

        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public string? BalanceTypeString { get; set; }
        public virtual VoteDetailLimitedresource? AssetLedgerAccount { get; set; }
        public virtual VoteDetailLimitedresource? GrantLedgerAccount { get; set; }

    }
}