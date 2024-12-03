using CAT20.Core.Models.Control;
using CAT20.Core.Models.TradeTax;
using CAT20.WebApi.Resources.TradeTax;
using System;
using System.Collections.Generic;

namespace CAT20.WebApi.Resources.Control
{
    public partial class BusinessResource
    {
        public BusinessResource()
        {
        }
        public int? Id { get; set; }
        public string BusinessName { get; set; }
        public string? BusinessSubOwners { get; set; }
        public BusinessNatureResource? BusinessNature { get; set; }
        public int? BusinessNatureId { get; set; }
        public BusinessSubNatureResource? BusinessSubNature { get; set; }
        public int? BusinessSubNatureId { get; set; }
        public DateTime? BusinessStartDate { get; set; }
        public string? BusinessRegNo { get; set; }
        public TaxType TaxType { get; set; }
        public int? TaxTypeId { get; set; }
        public string? BusinessTelNo { get; set; }
        public string? BusinessEmail { get; set; }
        public string? BusinessWeb { get; set; }
        public int? NoOfEmployees { get; set; }
        //public int? LastYearValue { get; set; }
        //public int? CurrentYear { get; set; }
        //public decimal? OtherCharges { get; set; }
        //public int? AnnualValue { get; set; }
        //public decimal? TaxAmountByNature { get; set; }
        //public decimal? TaxAmount { get; set; }
        //public decimal? TotalTaxAmount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public sbyte? Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? SabhaId { get; set; }
        public int? OfficeId { get; set; }
        public Partner? BusinessOwner { get; set; }
        public Partner? PropertyOwner { get; set; }
        public BusinessPlace? BusinessPlace { get; set; }
        public int? BusinessOwnerId { get; set; }
        public int? PropertyOwnerId { get; set; }
        public int? BusinessPlaceId { get; set; }
        //public string? ApplicationNo { get; set; }
        //public string? LicenseNo { get; set; }
        //public int? TaxState { get; set; }

        public virtual List<BusinessTaxesResource> BusinessTaxes { get; set; }
    }
}