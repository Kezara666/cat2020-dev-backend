using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.ShopRental;
using CAT20.WebApi.Resources.Control;
using System;
using System.Collections.Generic;

namespace CAT20.WebApi.Resources.ShopRental
{
    public partial class ShopFullResource
    {
        public ShopFullResource()
        {
            //VoteAssignments = new HashSet<VoteAssignmentResource>();
        }

        public int? Id { get; set; }
        public int? PropertyId { get; set; } //FK
        public string? BusinessName { get; set; }
        public string? BusinessNature { get; set; }
        public string? BusinessRegistrationNo { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerDesigntion { get; set; }
        public string? AgreementNo { get; set; }
        public DateOnly? AgreementStartDate { get; set; }
        public DateOnly? AgreementEndDate { get; set; }
        public decimal? Rental { get; set; }
        public decimal? KeyMoney { get; set; }
        public decimal? SecurityDeposit { get; set; }
        public decimal? ServiceCharge { get; set; }
        public ShopStatus? Status { get; set; }
        public int? IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }

        public virtual PropertyFullResource? Property { get; set; }
        public virtual ShopRentalVoteAssign? VoteAssign { get; set; }
        public virtual OpeningBalanceResource? OpeningBalance { get; set; } //Mapping 1(Shop): 1 (OpeningBalance)

        //Mapping 1(Shop): 1 (ProcessConfigurationSettingAssign)
        public virtual ProcessConfigurationSettingAssign? ProcessConfigurationSettingAssign { get; set; }

        public virtual ICollection<ShopRentalBalance>? Balances { get; set; }

        //public virtual RentalPlaceResource? RentalPlace { get; set; }
        public virtual OfficeResource Office { get; set; }
        public virtual PartnerResource Customer { get; set; }

        public virtual ShopStatus ShopStatus { get; set; }  
    }
}
