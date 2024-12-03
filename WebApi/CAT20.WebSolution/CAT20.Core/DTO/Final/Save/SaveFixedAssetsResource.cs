using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveFixedAssetsResource
    {
        public int? Id { get; set; }
        public int? AssetsLedgerAccountId { get; set; }
        public int? CustomVoteId { get; set; }
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

        //public DateTime? SystemActionAt { get; set; }
    }
}
