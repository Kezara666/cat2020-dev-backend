using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CAT20.Core.Models.ShopRental
{
    public partial class Shop
    {
        public Shop()
        {
            //OpeningBalances = new HashSet<OpeningBalance>();

            //---------------
            Balances = new HashSet<ShopRentalBalance>();
            //---------------
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
        public DateOnly? AgreementCloseDate { get; set; } //new (Shop agreement change request)
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


        //Mapping 1(property): many (shop)
        public virtual Property? Property { get; set; }

        //-------------
        //Mapping 1(Shop): 1 (VoteAssign)
        [JsonIgnore]
        public virtual ShopRentalVoteAssign? VoteAssign { get; set; }
        //-------------

        //-------------
        //Mapping 1(Shop): 1 (ShopRentalRecievableIncomeVoteAssign)
        [JsonIgnore]
        public virtual ShopRentalRecievableIncomeVoteAssign? RecievableIncomeVoteAssign { get; set; }
        //-------------

        //Mapping 1(Shop): 1 (OpeningBalance)
        [JsonIgnore]
        public virtual OpeningBalance? OpeningBalance { get; set; }

        //----
        //Mapping 1(shop): many (balances)
        [JsonIgnore]
        public virtual ICollection<ShopRentalBalance>? Balances { get; set; }
        //----


        //Mapping 1(Shop): 1 (ShopAgreementChangeRequest)
        [JsonIgnore]
        public virtual ShopAgreementChangeRequest? ShopAgreementChangeRequest { get; set; }

        //Mapping 1(Shop): 1 (ProcessConfigurationSettingAssign)
        [JsonIgnore]
        public virtual ProcessConfigurationSettingAssign? ProcessConfigurationSettingAssign { get; set; }

        //----------------------------------------------------
        public virtual Office? Office { get; set; } //Ignore field
        
        public virtual Partner? Customer { get; set; } //Ignore field
        //----------------------------------------------------
    }
}
