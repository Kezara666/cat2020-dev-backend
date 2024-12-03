using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveFixedAssetsDisposalResource
    {
        public int? Id { get; set; }
        //public int? AssetsLedgerAccountId { get; set; }
        //[Required]
        //public int? CustomVoteId { get; set; }
        //public string? AssetsTitle { get; set; }
        //public string? AssetsRegNo { get; set; }

        //public FixedAssetsBalanceTypes BalanceType { get; set; }

        //public DateTime? AcquiredDate { get; set; }
        //public DateTime? RevalueDate { get; set; }
        //public int? RemainingLifetime { get; set; }

        //public int? GrantLedgerAccountId { get; set; }
        //public int? GrantCustomVoteId { get; set; }


        //[Precision(18, 2)]
        //public decimal OriginalORRevaluedAmount { get; set; }
        //[Precision(18, 2)]
        //public decimal AccumulatedDepreciation { get; set; }

        //[Precision(18, 2)]
        //public decimal GrantAmount { get; set; }

        //[Precision(18, 2)]
        //public decimal AccumulatedRevenueRecognition { get; set; }


        //public int? OfficeId { get; set; }
        //public int? SabhaId { get; set; }
        public int Status { get; set; }
        //public int? Year { get; set; }
        //public int? ParentId { get; set; }

        public decimal? SaleOrScrapAmount { get; set; }

        //public decimal? ProfitOrLoss { get; set; }

        public DateTime? DisposalDate { get; set; }


    }
}
