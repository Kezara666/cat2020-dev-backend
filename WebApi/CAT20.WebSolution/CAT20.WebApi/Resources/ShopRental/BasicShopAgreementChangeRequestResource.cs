using CAT20.Core.Models.Enums;

namespace CAT20.WebApi.Resources.ShopRental
{
    public partial class BasicShopAgreementChangeRequestResource
    {
        public int? Id { get; set; }

        public int ShopId { get; set; } //FK

        public int? Requestedstatus { get; set; }  //Active = 1, | Inactive = 2,

        public DateOnly? AgreementCloseDate { get; set; }

        public DateOnly? AgreementExtendEndDate { get; set; }

        public ShopAgreementChangeRequestType? RequestType { get; set; }

        //mapping--------------------------------------
        public virtual BasicShopResource? Shop { get; set; }  //Mapping 1(Shop): 1 (AgrrementChangeRequest)
        //---------------------------------------------

        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public string? AgreementChangeReason { get; set; }
        public DateTime ApprovedAt { get; set; }
        public int? ApprovedBy { get; set; }
        public string? ApproveComment { get; set; }
        public int ApproveStatus { get; set; } //0-pending | 1-approved | 2-rejected
    }
}
