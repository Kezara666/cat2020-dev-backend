using CAT20.Core.Models.Control;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAT20.Core.Models.ShopRental
{
    public class ShopRentalBalance
    {

        [Key]
        public int? Id { get; set; } //primary key

        [Required]
        public int? PropertyId { get; set; } //Foreign Key field

        [Required]
        public int? ShopId { get; set; } //Foreign Key field

        [Required]
        public int Year { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public DateOnly FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        [Required]
        public DateOnly BillProcessDate { get; set; }

        [Precision(18, 2)]
        public decimal? ArrearsAmount { get; set; }

        [Precision(18, 2)]
        public decimal? PaidArrearsAmount { get; set; }

        [Precision(18, 2)]
        public decimal? FineAmount { get; set; }

        [Precision(18, 2)]
        public decimal? PaidFineAmount { get; set; }

        [Precision(18, 2)]
        public decimal? ServiceChargeArreasAmount { get; set; }

        [Precision(18, 2)]
        public decimal? PaidServiceChargeArreasAmount { get; set; }

        //[Precision(18, 2)]
        //public decimal? CurrentServiceChargeAmount { get; set; }

        //[Precision(18, 2)]
        //public decimal? PaidCurrentServiceChargeAmount { get; set; } //no need

        //[Precision(18, 2)]
        //public decimal? CurrentRentalAmount { get; set; }

        //[Precision(18, 2)]
        //public decimal? PaidCurrentRentalAmount { get; set; } //no need

        [Precision(18, 2)]
        public decimal? OverPaymentAmount { get; set; }

        //[Precision(18, 2)]
        //public decimal? OnTimePaid { get; set; } //no need

        //[Precision(18, 2)]
        //public decimal? LatePaid { get; set; } //no need

        //[Precision(18, 2)]
        //public decimal? Payments { get; set; } //no need

        [Required]
        public bool IsCompleted { get; set; }

        //[Required]
        //public bool IsFilled { get; set; } //no need

        [Required]
        public bool? IsProcessed { get; set; } = false;

        //[Required]
        public int? NoOfPayments { get; set; } ///

        // mandatory fields
        public int? Status { get; set; }
        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        //Mapping 1(property): many (balances)
        public virtual Property? Property { get; set; }

        //Mapping 1(shop): many (balances)   
        public virtual Shop? Shop { get; set; }

        //---------
        //not mapping fields ----- ignore field
        public virtual Partner? Customer { get; set; }
        //---------


        //-----new
        public bool? HasTransaction { get; set; } //modified 2024/04/09










        //------[Start: fields for Report]-------
        [Precision(18, 2)]
        public decimal? LYFine { get; set; }

        [Precision(18, 2)]
        public decimal? PaidLYFine { get; set; }
        //----

        [Precision(18, 2)]
        public decimal? LYArreas { get; set; }

        [Precision(18, 2)]
        public decimal? PaidLYArreas { get; set; }
        //----

        [Precision(18, 2)]
        public decimal? TYFine { get; set; }

        [Precision(18, 2)]
        public decimal? PaidTYFine { get; set; }
        //----

        [Precision(18, 2)]
        public decimal? TYArreas { get; set; }

        [Precision(18, 2)]
        public decimal? PaidTYArreas { get; set; }
        //----

        [Precision(18, 2)]
        public decimal? TYLYServiceChargeArreas { get; set; }

        [Precision(18, 2)]
        public decimal? PaidTYLYServiceChargeArreas { get; set; }
        //----

        [Precision(18, 2)]
        public decimal? CurrentServiceChargeAmount { get; set; }

        [Precision(18, 2)]
        public decimal? PaidCurrentServiceChargeAmount { get; set; }

        [Precision(18, 2)]
        public decimal? CurrentRentalAmount { get; set; }

        [Precision(18, 2)]
        public decimal? PaidCurrentRentalAmount { get; set; }

        [Precision(18, 2)]
        public decimal? CurrentMonthNewFine { get; set; }

        [Precision(18, 2)]
        public decimal? PaidCurrentMonthNewFine { get; set; }
        //------[End: fields for Report]---------

        public int? SettledMixinOrderId { get; set; }

        public int? IsHold { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
