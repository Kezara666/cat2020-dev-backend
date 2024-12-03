using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.Models.FinalAccount
{
    public class FixedAssets
    {
                /*status
                 * 
             0 deleted
        1 active
        2 deprecated
        3 disposeTransfered
        4 disposeSold
                 */
        public int? Id { get; set; }
        public int? AssetsLedgerAccountId { get; set; }
        [Required]
        public int? CustomVoteId { get; set; }
        public string? AssetsTitle { get; set; }
        public string? AssetsRegNo { get; set; }

        public FixedAssetsBalanceTypes BalanceType { get; set; }

        public DateTime? AcquiredDate { get; set; }
        public DateTime? RevalueDate { get; set; }
        public int? RemainingLifetime { get; set; }

        public int? GrantLedgerAccountId {  get; set; }
        public int? GrantCustomVoteId {  get; set; }


        [Precision(18, 2)]
        public decimal OriginalORRevaluedAmount { get; set; }
        [Precision(18, 2)]
        public decimal AccumulatedDepreciation { get; set; }

        [Precision(18, 2)]
        public decimal GrantAmount { get; set; }

        [Precision(18, 2)]
        public decimal AccumulatedRevenueRecognition { get; set; }

        [NotMapped]
        public int Year { get; set; }
        [NotMapped]
        public int ParentId { get; set; }

        [NotMapped]
        //[Required]
        [Precision(18, 2)]
        public decimal SaleOrScrapAmount { get; set; }

        [NotMapped]
        //[Required]
        [Precision(18, 2)]
        public decimal ProfitOrLoss { get; set; }

        [NotMapped]
        public DateTime? DisposalDate { get; set; }

        /*mandatory filed*/

        [Required]
        public int? OfficeId { get; set; }
        [Required]
        public int? SabhaId { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? SystemActionAt { get; set; }
    }
}
