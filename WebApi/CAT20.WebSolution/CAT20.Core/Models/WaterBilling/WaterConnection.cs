using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.WaterBilling
{
    [Index(nameof(OfficeId))]
    public class WaterConnection
    {
        [Key]
        public int? Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string? ConnectionId { get; set; }
        [JsonIgnore]
        public virtual MeterConnectInfo? MeterConnectInfo { get; set; }

        [Required]
        public int? PartnerId { get; set; }
        [Required]
        public int? BillingId { get; set; }

        public virtual WaterProjectSubRoad? SubRoad { get; set; }

        [Required]
        public int? SubRoadId { get; set; } // Foreign key

        [Required]
        public int? OfficeId { get; set; } // Foreign key


        [Required]
        public DateOnly? InstallDate { get; set; }

        public int ActiveStatus { get; set; }
        public int ActiveNatureId { get; set; }
        public virtual WaterProjectNature? ActiveNature { get; set; }

        public bool? StatusChangeRequest { get; set; }
        public bool? NatureChangeRequest { get; set; }


        // Navigation property to represent the one-to-one relationship

        public virtual OpeningBalanceInformation? OpeningBalanceInformation { get; set; }
        // Navigation property to represent the one-to-many relationship


        public virtual ICollection<WaterConnectionBalance>? Balances { get; set; }
        public virtual ICollection<WaterConnectionBalanceHistory>? BalanceHistory { get; set; }

        [JsonIgnore]
        public virtual ICollection<WaterConnectionNatureLog>? NatureInfos { get; set; }

        [JsonIgnore]
        public virtual ICollection<WaterConnectionStatusLog>? StatusInfos { get; set; }
        [JsonIgnore]

        public virtual ICollection<WaterBillDocument>? Documents { get; set; }
        public virtual ICollection<ConnectionAuditLog>? ConnectionAuditLogs { get; set; }
        public virtual ICollection<WaterMonthEndReport>? WaterMonthEndReports { get; set; }



        //additional one to one filed 

        [Precision(18, 2)]
        public decimal? RunningOverPay { get; set; }
        [Precision(18, 2)]
        public decimal? RunningVatRate { get; set; }



        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
