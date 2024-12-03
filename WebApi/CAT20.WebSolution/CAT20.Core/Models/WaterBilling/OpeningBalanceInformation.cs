using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.WaterBilling
{
    public class OpeningBalanceInformation
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public int WaterConnectionId { get; set; }
        [JsonIgnore]
        public virtual WaterConnection? WaterConnection { get; set; }
        public virtual ICollection<OBLIApprovalStatus>? OBLIApprovalStatus { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? LastYearArrears { get; set; }


        [Required]
        [Precision(18, 2)]
        public decimal? MonthlyBalance { get; set; }

        [Required]
        public int? LastMeterReading { get; set; }

        public int? BalanceIdForLastYearArrears { get; set; }
        [Required]
        public int? BalanceIdForCurrentBalance { get; set; }

        // mandatory fields
        public int? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? IsProcessed { get; set; } = false;
    }
}
