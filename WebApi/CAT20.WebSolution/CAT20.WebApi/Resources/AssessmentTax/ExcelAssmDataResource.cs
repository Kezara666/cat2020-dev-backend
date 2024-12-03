using System;
using System.Collections.Generic;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Control;
using CAT20.WebApi.Resources.AssessmentTax;
using CAT20.WebApi.Resources.Control;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public partial class ExcelAssmDataResource
    {
        public int streetId { get; set; }
        public int wardId { get; set; }
        public int assessmentPropertyTypeId { get; set; }
        public int descriptionId { get; set; }
        public int? assessmentOrder { get; set; }
        public string assessmentNo { get; set; }
        public int? assessmentStatus { get; set; }
        public string? assessmentComment { get; set; }
        public string? assessmentObsolete { get; set; }
        public int? officeId { get; set; }
        public int? sabhaId { get; set; }
        public int? isWarrant { get; set; }
        public int? createdBy { get; set; }
        public string? subOwner_name { get; set; }
        public string? subOwner_nICNumber { get; set; }
        public string? subOwner_title { get; set; }
        public int? allocation_allocationAmount { get; set; }
        public DateTime? allocation_changedDate { get; set; }
        public string? allocation_allocationDescription { get; set; }
        public int? opnbal_oBYear { get; set; }
        public int? opnbal_quarterNumber { get; set; }
        //public DateTime? opnbal_processDate { get; set; }
        public decimal? opnbal_lYArreas { get; set; }
        public decimal? opnbal_lYWarrant { get; set; }
        public decimal? opnbal_overPayment { get; set; }
        //public int? opnbal_lYCArreas { get; set; }
        //public int? opnbal_lQArreas { get; set; }
        //public int? opnbal_lQWarrant { get; set; }
        //public int? opnbal_lQCArreas { get; set; }
        //public int? opnbal_lQCWarrant { get; set; }
        //public int? opnbal_haveToQPay { get; set; }
        //public int? opnbal_qPay { get; set; }
        //public int? opnbal_qDiscont { get; set; }
        //public int? opnbal_qTotal { get; set; }
        //public int? opnbal_fullTotal { get; set; }
        //public int? opnbal_processUpdateWarrant { get; set; }
        //public int? opnbal_processUpdateArrears { get; set; }
        //public string? opnbal_processUpdateComment { get; set; }
        //public int? opnbal_oldArrears { get; set; }
        //public int? opnbal_oldWarrent { get; set; }
        public string? tempPartner_name { get; set; }
        public string? tempPartner_nicNumber { get; set; }
        public string? tempPartner_mobileNumber { get; set; }
        public string? tempPartner_phoneNumber { get; set; }
        public string? tempPartner_street1 { get; set; }
        public string? tempPartner_street2 { get; set; }
        public string? tempPartner_city { get; set; }
        public string? tempPartner_zip { get; set; }
        public string? tempPartner_email { get; set; }
    }
}
