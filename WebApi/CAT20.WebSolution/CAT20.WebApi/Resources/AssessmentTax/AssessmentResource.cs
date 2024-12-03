using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.WebApi.Resources.Control;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public partial class AssessmentResource
    {

        public int? Id { get; set; }
        public int? PartnerId { get; set; }
        public virtual PartnerResource? Partner { get; set; }
        public virtual PartnerResource? SubPartner { get; set; }
        public int? SubPartnerId { get; set; }
        public int? StreetId { get; set; }

        public int? PropertyTypeId { get; set; }
        public int? DescriptionId { get; set; }
        public int? OrderNo { get; set; }
        public string? AssessmentNo { get; set; }
        public int? AssessmentStatus { get; set; }
        public int? Syn { get; set; }
        public string? Comment { get; set; }
        public string? Obsolete { get; set; }

        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public bool? IsWarrant { get; set; }
        public int? TempPartnerId { get; set; }
        public int? TempSubPartnerId { get; set; }

        public bool IsPartnerUpdated { get; set; }
        public bool IsSubPartnerUpdated { get; set; }

        public bool PropertyTypeChangeRequest { get; set; }
        public bool DescriptionChangeRequest { get; set; }

        public bool AllocationChangeRequest { get; set; }
        public bool DeleteRequest { get; set; }
        public bool HasJournalRequest { get; set; }
        public bool HasAssetsChangeRequest { get; set; }


        public HAssessmentBalance? PayableBalance { get; set; }

        public virtual DescriptionResource? Description { get; set; }
        public virtual AssessmentPropertyTypeResource? AssessmentPropertyType { get; set; }
        public virtual StreetResource? Street { get; set; }
        public virtual AllocationResource? Allocation { get; set; }
        public virtual AssessmentTempPartnerResource? AssessmentTempPartner { get; set; }
        public virtual ICollection<AssessmentTempSubPartnerResource>? AssessmentTempSubPartner { get; set; }
        public virtual AssessmentBalanceResource? AssessmentBalance { get; set; }
        public virtual NewAllocationRequestResource? NewAllocationRequest { get; set; }

        public virtual ICollection<AssessmentPropertyTypeLogResource>? AssessmentPropertyTypeLogs { get; set; }
        public virtual ICollection<AssessmentDescriptionLogResource>? AssessmentDescriptionLogs { get; set; }

        public virtual ICollection<AssessmentBalancesHistoryResources>? AssessmentBalanceHistories { get; set; }

        public virtual ICollection<AssessmentJournalResource>? AssessmentJournals { get; set; }

        public virtual ICollection<AssessmentAssetsChangeResource>? AssessmentAssetsChange { get; set; }
        // mandatory fields

        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        //public virtual Partner? Partner { get; set; }
        //public virtual Partner? SubPartner { get; set; }

        /*linking model*/

        public virtual OfficeResource? Office { get; set; }



    }
}
