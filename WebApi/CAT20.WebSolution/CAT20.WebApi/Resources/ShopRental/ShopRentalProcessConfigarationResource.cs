using CAT20.Core.Models.ShopRental;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.ShopRental
{
    public partial class ShopRentalProcessConfigarationResource
    {
        public int? Id { get; set; }
        public int? SabhaId { get; set; }
        //-----------------------------------------
        public string? Name { get; set; }
        public int FineRateTypeId { get; set; } //FK

        public decimal? FineDailyRate { get; set; }

        public decimal? FineMonthlyRate { get; set; }

        public decimal? Fine1stMonthRate { get; set; }

        public decimal? Fine2ndMonthRate { get; set; }

        public decimal? Fine3rdMonthRate { get; set; }

        public decimal? FineFixAmount { get; set; }
        //-----------------------------------------
        public int FineDate { get; set; } //This is a value like 10
        public int RentalPaymentDateTypeId { get; set; } //FK
        public int FineCalTypeId { get; set; } //Fk
        public int FineChargingMethodId { get; set; } //FK
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        //Mapping---
        public virtual FineRateTypeResource? FineRateType { get; set; }

        public virtual FineCalTypeResource? FineCalType { get; set; }

        public virtual RentalPaymentDateTypeResource? RentalPaymentDateType { get; set; }

        public virtual FineChargingMethodResource? FineChargingMethod { get; set; }
        //----

        //public virtual ICollection<ProcessConfigurationSettingAssign>? ProcessConfigurationSettingAssigns { get; set; }
        ////-------------------
        public virtual ICollection<ShopRentalProcess>? ShopRentalProcess { get; set; }
    }
}
