using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Mixin;
using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class BusinessTaxes
    {
        public int? Id { get; set; }
        public virtual Business? Business { get; set; }
        public int BusinessId { get; set; }
        public int? CurrentYear { get; set; }
        public string? ApplicationNo { get; set; }
        public string? LicenseNo { get; set; }
        public decimal? LastYearValue { get; set; }
        public decimal? AnnualValue { get; set; }
        public decimal? OtherCharges { get; set; }
        public decimal? TaxAmountByNature { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TotalTaxAmount { get; set; }
        public int? SabhaId { get; set; }
        public int? OfficeId { get; set; }
        public sbyte? Status { get; set; }
        public TaxStatus TaxState { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public sbyte? is_moh_approved { get; set; }
        public sbyte? is_secretary_approved { get; set; }
        public sbyte? is_chairman_approved { get; set; }
        public int? MOHApprovedBy { get; set; }
        public int? SecretaryApprovedBy { get; set; }
        public int? ChairmanApprovedBy { get; set; }
        public DateTime? MOHApprovedAt { get; set; }
        public DateTime? SecretaryApprovedAt { get; set; }
        public DateTime? ChairmanApprovedAt { get; set; }
    }
}