using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.ShopRental
{
    public partial class OpeningBalance
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int PropertyId { get; set; } //FK

        [Required]
        public int ShopId { get; set; } //FK

        [Required]
        public int Year { get; set; }

        [Required]
        public int MonthId { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? LastYearArrearsAmount { get; set; } //Last year arreas amount

        [Required]
        [Precision(18, 2)]
        public decimal? ThisYearArrearsAmount { get; set; } //This year arreas amount

        [Required]
        [Precision(18, 2)]
        public decimal? LastYearFineAmount { get; set; } //Last year fine amount

        [Required]
        [Precision(18, 2)]
        public decimal? ThisYearFineAmount { get; set; } //This year fine amount

        [Required]
        [Precision(18, 2)]
        public decimal? OverPaymentAmount { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? ServiceChargeArreasAmount { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? CurrentServiceChargeAmount { get; set; } //Monthly service charge

        //---------- update ------
        [Required]
        [Precision(18, 2)]
        public decimal? CurrentRentalAmount { get; set; } //Monthly rental
                                                          //---------- update ------

        //----
        public int? BalanceIdForLastYearArrears { get; set; }

        [Required]
        public int? BalanceIdForCurrentBalance { get; set; }
        //----

        public int Status { get; set; }  
        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        //mapping--------------------------------------
        public virtual Property? Property { get; set; } //Mapping 1(Property): many (OpeningBalance)
        public virtual Shop? Shop { get; set; }  //Mapping 1(Shop): 1 (OpeningBalance)
        //---------------------------------------------

        public bool? IsProcessed { get; set; } = false; //Bill processed?

        //new fields
        public DateTime ApprovedAt { get; set; }
        public int? ApprovedBy { get; set; }
        public string? ApproveComment { get; set; }
        public int ApproveStatus { get; set; } //0-pending | 1-approved | 2-rejected
    }
}
