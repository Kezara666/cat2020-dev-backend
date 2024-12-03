using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.Models.ShopRental
{
    public class ShopRentalBalanceLog
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
        public decimal? PriviousArrearsAmount { get; set; }
        [Precision(18, 2)]
        public decimal? NewArrearsAmount { get; set; }

        [Precision(18, 2)]
        public decimal? PreviousPaidArrearsAmount { get; set; }
        [Precision(18, 2)]
        public decimal? NewPaidArrearsAmount { get; set; }

        [Precision(18, 2)]
        public decimal? PreviousFineAmount { get; set; }
        [Precision(18, 2)]
        public decimal? NewFineAmount { get; set; }

        [Precision(18, 2)]
        public decimal? PreviousPaidFineAmount { get; set; }
        [Precision(18, 2)]
        public decimal? NewPaidFineAmount { get; set; }

        [Precision(18, 2)]
        public decimal? PreviousServiceChargeArreasAmount { get; set; }
        [Precision(18, 2)]
        public decimal? NewServiceChargeArreasAmount { get; set; }

        [Precision(18, 2)]
        public decimal? PreviousPaidServiceChargeArreasAmount { get; set; }
        [Precision(18, 2)]
        public decimal? NewPaidServiceChargeArreasAmount { get; set; }



        [Precision(18, 2)]
        public decimal? PreviousOverPaymentAmount { get; set; }
        [Precision(18, 2)]
        public decimal? NewOverPaymentAmount { get; set; }



        [Required]
        public bool IsCompleted { get; set; }

       
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
        public decimal? PreviousLYFine { get; set; }
        [Precision(18, 2)]
        public decimal? NewLYFine { get; set; }


        [Precision(18, 2)]
        public decimal? PreviousPaidLYFine { get; set; }
        [Precision(18, 2)]
        public decimal? NewPaidLYFine { get; set; }

        //----

        [Precision(18, 2)]
        public decimal? PreviousLYArreas { get; set; }
        [Precision(18, 2)]
        public decimal? NewLYArreas { get; set; }

        [Precision(18, 2)]
        public decimal? PreviousPaidLYArreas { get; set; }
        [Precision(18, 2)]
        public decimal? NewPaidLYArreas { get; set; }

        //----

        [Precision(18, 2)]
        public decimal? PreviousTYFine { get; set; }
        [Precision(18, 2)]
        public decimal? NewTYFine { get; set; }

        [Precision(18, 2)]
        public decimal? PreviousPaidTYFine { get; set; }

        [Precision(18, 2)]
        public decimal? NewPaidTYFine { get; set; }

        //----

        [Precision(18, 2)]
        public decimal? PreviousTYArreas { get; set; }
        [Precision(18, 2)]
        public decimal? NewTYArreas { get; set; }

        [Precision(18, 2)]
        public decimal? PreviousPaidTYArreas { get; set; }
        [Precision(18, 2)]
        public decimal? NewPaidTYArreas { get; set; }

        //----

        [Precision(18, 2)]
        public decimal? PreviousTYLYServiceChargeArreas { get; set; }
        [Precision(18, 2)]
        public decimal? NewTYLYServiceChargeArreas { get; set; }

        [Precision(18, 2)]
        public decimal? PreviousPaidTYLYServiceChargeArreas { get; set; }
        [Precision(18, 2)]
        public decimal? NewPaidTYLYServiceChargeArreas { get; set; }

        //----

        [Precision(18, 2)]
        public decimal? PreviousCurrentServiceChargeAmount { get; set; }
        [Precision(18, 2)]
        public decimal? NewCurrentServiceChargeAmount { get; set; }


        [Precision(18, 2)]
        public decimal? PreviousPaidCurrentServiceChargeAmount { get; set; }
        [Precision(18, 2)]
        public decimal? NewPaidCurrentServiceChargeAmount { get; set; }

        [Precision(18, 2)]
        public decimal? PreviousCurrentRentalAmount { get; set; }
        [Precision(18, 2)]
        public decimal? NewCurrentRentalAmount { get; set; }

        [Precision(18, 2)]
        public decimal? PreviousPaidCurrentRentalAmount { get; set; }
        [Precision(18, 2)]
        public decimal? NewPaidCurrentRentalAmount { get; set; }

        [Precision(18, 2)]
        public decimal? PreviousCurrentMonthNewFine { get; set; }
        [Precision(18, 2)]
        public decimal? NewCurrentMonthNewFine { get; set; }

        [Precision(18, 2)]
       
        public decimal? PreviousPaidCurrentMonthNewFine { get; set; }
        [Precision(18, 2)]
        public decimal? NewPaidCurrentMonthNewFine { get; set; }

        //------[End: fields for Report]---------

        public int? SettledMixinOrderId { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
