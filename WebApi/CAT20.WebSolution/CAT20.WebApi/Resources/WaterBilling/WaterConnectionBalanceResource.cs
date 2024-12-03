using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAT20.WebApi.Resources.WaterBilling
{
    public class WaterConnectionBalanceResource
    {
        public int? Id { get; set; }
        public int? WcPrimaryId { get; set; }
        //public virtual WaterConnection? WaterConnection { get; set; }


        public string? BarCode { get; set; }
        public string? InvoiceNo { get; set; }


        public int Year { get; set; }

        public int Month { get; set; }


        public DateOnly FromDate { get; set; }



        public DateOnly? ToDate { get; set; }
        public int? ReadBy { get; set; }
        public DateOnly BillProcessDate { get; set; }

        public string? MeterNo { get; set; }

        public int? PreviousMeterReading { get; set; }

        public int? ThisMonthMeterReading { get; set; }

        public DateOnly? ReadingDate { get; set; }

        public decimal? WaterCharge { get; set; }
        public decimal? FixedCharge { get; set; }

        public decimal? VATRate { get; set; }

        public decimal? VATAmount { get; set; }


        public decimal? ThisMonthCharge { get; set; }

        public decimal? ThisMonthChargeWithVAT { get; set; }

        public decimal? TotalDue { get; set; }

        public int? MeterCondition { get; set; }

        public int? AdditionalType { get; set; }

        public decimal? AdditionalCharges { get; set; }

        public decimal? ByExcessDeduction { get; set; }


        public decimal? OnTimePaid { get; set; }

        public decimal? LatePaid { get; set; }

        public decimal? OverPay { get; set; }

        public decimal? Payments { get; set; }

        public bool? IsCompleted { get; set; }

        public bool? IsFilled { get; set; }

        public bool? IsProcessed { get; set; } = false;


        //Print Info 

        public string? LastBillYearMonth { get; set; }

        public string? PrintBillingDetails { get; set; }

        public decimal? PrintBalanceBF { get; set; }

        public decimal? PrintLastMonthPayments { get; set; }
        public decimal? PrintLastBalance { get; set; }
        public int? NumPrints { get; set; }

        // mandatory fields

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }


        /*not map amount*/

        public int? SessionId { get; set; } = 0;
        public decimal? Payable { get; set; }
        public decimal? PayingAmount { get; set; }
        public decimal? PayingVatAmount { get; set; }
    }
}
