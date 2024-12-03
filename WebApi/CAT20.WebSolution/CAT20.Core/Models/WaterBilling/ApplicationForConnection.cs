using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.WaterBilling
{
    public class ApplicationForConnection
    {
        [Key]
        [MaxLength(10)]
        public string? ApplicationNo { get; set; }
        [Required]
        public int? PartnerId { get; set; }
        [Required]
        public int BillingId { get; set; }

        [Required]
        public int RequestedNatureId { get; set; }
        [JsonIgnore]
        public virtual WaterProjectNature? Nature { get; set; }

        public int? RequestedConnectionId { get; set; }
        public bool? OnlyBillingChange { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsAssigned { get; set; }
        [MaxLength(100)]
        public string? Comment { get; set; }

        public int? ApprovedBy { get; set; }

        [JsonIgnore]
        public virtual WaterProjectSubRoad? SubRoad { get; set; }
        [Required]
        public int? SubRoadId { get; set; } // Foreign key

        public int? ApplicationType { get; set; }
        // Navigation property to represent the one-to-many relationship
        [JsonIgnore]
        public virtual ICollection<ApplicationForConnectionDocument>? SubmittedDocuments { get; set; }


        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public DateTime? ApprovedAt { get; set; } //new

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }


    }
}
