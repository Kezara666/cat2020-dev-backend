using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.ShopRental
{
    public class ShopRentalProcessConfigaration
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? SabhaId { get; set; }

        //-----------------------------------------
        [Required]
        public string? Name { get; set; }
        //-----
        [Required]
        public int FineRateTypeId { get; set; } //FK

        public decimal? FineDailyRate { get; set; }

        public decimal? FineMonthlyRate { get; set; }

        public decimal? Fine1stMonthRate { get; set; }

        public decimal? Fine2ndMonthRate { get; set; }

        public decimal? Fine3rdMonthRate { get; set; }

        public decimal? FineFixAmount { get; set; }
        //-----------------------------------------

        [Required]
        public int FineDate { get; set; } //This is a value like 10

        [Required]
        public int RentalPaymentDateTypeId { get; set; } //FK

        [Required]
        public int FineCalTypeId { get; set; } //FK

        [Required]
        public int FineChargingMethodId { get; set; } //FK

        public int? Status { get; set;}
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        //ignore field
        public virtual Sabha? Sabha { get; set; } //Ignore field


        //------Mapping------
        public virtual FineRateType? FineRateType { get; set; }

        public virtual FineCalType? FineCalType { get; set; }

        public virtual RentalPaymentDateType? RentalPaymentDateType { get; set; }

        public virtual FineChargingMethod? FineChargingMethod { get; set; }


        //Mapping 1(ShopRentalProcessConfigaration): Many (ProcessConfigurationSettingAssign)
        public virtual ICollection<ProcessConfigurationSettingAssign>? ProcessConfigurationSettingAssigns { get; set; }
        //-------------------
        public virtual ICollection<ShopRentalProcess>? ShopRentalProcess { get; set; }
    }
}
