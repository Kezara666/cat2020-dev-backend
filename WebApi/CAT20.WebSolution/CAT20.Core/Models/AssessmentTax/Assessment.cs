
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax
{
    public partial class Assessment
    {

        [Key]
        public int? Id { get; set; }
        public int? PartnerId { get; set; }
        public int? SubPartnerId { get; set; }
        [JsonIgnore]
        public virtual Partner? Partner { get; set; }
        [JsonIgnore]
        public virtual Partner? SubPartner { get; set; }

        [Required]
        public int? StreetId { get; set; }

        [Required]
        public int? PropertyTypeId { get; set; }
        [Required]
        public int? DescriptionId { get; set; }
        [Required]
        public int? OrderNo { get; set; }
        [Required]
        public string? AssessmentNo { get; set; }
        //public int? AssessmentStatus { get; set; }
        public AssessmentStatus? AssessmentStatus { get; set; }
        public int? Syn { get; set; }
        public string? Comment { get; set; }
        public string? Obsolete { get; set; }

        [Required]
        public int? OfficeId { get; set; }
        [Required]
        public int? SabhaId { get; set; }
        public bool? IsWarrant { get; set; }
        //public int? TempPartnerId { get; set; }
        //public int? TempSubPartnerId { get; set; }

        public bool IsPartnerUpdated { get; set; }
        public bool IsSubPartnerUpdated { get; set; }

        public bool PropertyTypeChangeRequest { get; set; }
        public bool DescriptionChangeRequest { get; set; }
        public bool AllocationChangeRequest { get; set; }
        public bool DeleteRequest { get; set; }
        public bool HasJournalRequest { get; set; }
        public bool HasAssetsChangeRequest { get; set; }
        public bool HasBillAdjustmentRequest { get; set; }

        public string? PropertyAddress { get; set; }
        public int? NextYearPropertyTypeId { get; set; }
        public int? NextYearDescriptionId { get; set; }
        public int? ParentAssessmentId { get; set; }


        // mandatory fields

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        //public virtual Partner? Partner { get; set; }
        //public virtual Partner? SubPartner { get; set; }

        [JsonIgnore]
        public virtual Description? Description { get; set; }
        [JsonIgnore]
        public virtual AssessmentPropertyType? AssessmentPropertyType { get; set; }
        [JsonIgnore]
        public virtual Street? Street { get; set; }
        [JsonIgnore]
        public virtual Allocation? Allocation { get; set; }
        [JsonIgnore]
        public virtual NewAllocationRequest? NewAllocationRequest { get; set; }
        [JsonIgnore]
        public virtual AssessmentTempPartner? AssessmentTempPartner { get; set; }
        [JsonIgnore]
        public virtual ICollection<AssessmentTempSubPartner>? AssessmentTempSubPartner { get; set; }
        [JsonIgnore]
        public virtual AssessmentBalance? AssessmentBalance { get; set; }
        [JsonIgnore]

        public virtual ICollection<AssessmentBalancesHistory>? AssessmentBalanceHistories { get; set; }
        [Required]
        public virtual ICollection<AssessmentTransaction>? Transactions { get; set; }
        public virtual ICollection<AssessmentAuditLog>? AssessmentAuditLogs { get; set; }
        public virtual ICollection<AssessmentPropertyTypeLog>? AssessmentPropertyTypeLogs { get; set; }
        public virtual ICollection<AssessmentDescriptionLog>? AssessmentDescriptionLogs { get; set; }
        public virtual ICollection<AssessmentDocument>? AssessmentDocuments { get; set; }
        [JsonIgnore]
        public virtual ICollection<AssessmentJournal>? AssessmentJournals { get; set; }
        public virtual ICollection<AssessmentAssetsChange>? AssessmentAssetsChange { get; set; }
        //public virtual ICollection<AssessmentATD>? AssessmentATD { get; set; }

        [JsonIgnore]
        public virtual ICollection<AssessmentQuarterReport>? AssessmentQuarterReport { get; set; }
        public virtual ICollection<AssessmentBillAdjustment>? AssessmentBillAdjustments { get; set; }
        public virtual ICollection<AmalgamationAssessment>? AmalgamationAssessment { get; set; }
        public virtual ICollection<AssessmentATD>? AssessmentATDs { get; set; }
    }
}
