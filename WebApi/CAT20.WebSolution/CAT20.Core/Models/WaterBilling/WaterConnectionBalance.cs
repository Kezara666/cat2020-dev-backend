using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAT20.Core.Models.WaterBilling
{
    public class WaterConnectionBalance
    {
      

        [Key]
        public int? Id { get; set; }
        [Required]
        public int? WcPrimaryId { get; set; }
        [JsonIgnore]
        public virtual WaterConnection? WaterConnection { get; set; }

        [Required]
        [StringLength(50)]
        public string? ConnectionNo { get; set; }

        [Required]
        public string? BarCode { get; set; }
        [Required]
        public string? InvoiceNo { get; set; }


        [Required]
        public int Year { get; set; }

        [Required]
        public int Month { get; set; }


        [Required]
        public DateOnly FromDate { get; set; }


        public DateOnly? ToDate { get; set; }

        public int? ReadBy { get; set; }

        [Required]
        public DateOnly BillProcessDate { get; set; }

        [Required]
        public string? MeterNo { get; set; }

        [Required]
        public int? PreviousMeterReading { get; set; }

        public int? ThisMonthMeterReading { get; set; }



        [Precision(18, 2)]
        public decimal? WaterCharge { get; set; }
        [Precision(18, 2)]
        public decimal? FixedCharge { get; set; }

        [Precision(18, 2)]
        public decimal? VATRate { get; set; }

        [Precision(18, 2)]
        public decimal? VATAmount { get; set; }


        [Precision(18, 2)]
        public decimal? ThisMonthCharge { get; set; }

        [Precision(18, 2)]
        public decimal? ThisMonthChargeWithVAT { get; set; }

        [Precision(18, 2)]
        public decimal? TotalDue { get; set; }

        public int? MeterCondition { get; set; }


        [Precision(18, 2)]
        public decimal? ByExcessDeduction { get; set; }

        [Precision(18, 2)]
        public decimal? OnTimePaid { get; set; }

        [Precision(18, 2)]
        public decimal? LatePaid { get; set; }

        [Precision(18, 2)]
        public decimal? OverPay { get; set; }


        [Precision(18, 2)]
        public decimal? Payments { get; set; }

        [Required]
        public bool? IsCompleted { get; set; }

        [Required]
        public bool? IsFilled { get; set; }

        [Required]
        public bool? IsProcessed { get; set; } = false;

        [Required]
        public int? NoOfPayments { get; set; }
        [Required]
        public int? NoOfCancels { get; set; }



        //Print Info 

        [Required]
        [Precision(18, 2)]
        public decimal? PrintLastBalance { get; set; }

        public string? CalculationString { get; set; }


        [Required]
        public string? LastBillYearMonth { get; set; }

        [Required]
        public string? PrintBillingDetails { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? PrintBalanceBF { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? PrintLastMonthPayments { get; set; }

        public int? NumPrints { get; set; }

        // mandatory fields

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }
       


        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }


        /*not map amount*/

        [NotMapped]
        public int? SessionId { get; set; } = 0;

        [NotMapped]
        public decimal? Payable { get; set; } = 0;
        [NotMapped]
        public decimal? PayableVAT { get; set; } = 0;

        [NotMapped]
        public decimal? PayingAmount { get; set; } = 0;
        [NotMapped]
        public decimal? PayingVatAmount { get; set; } = 0;


        [Precision(18, 2)]
        public decimal? LastYearArrears { get; set; }

        [Precision(18, 2)]
        public decimal? ThisYearArrears { get; set; }

        [Precision(18, 2)]
        public decimal? OverPayment { get; set; }




    }
}
