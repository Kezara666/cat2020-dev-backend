using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.WaterBilling
{
    public class WaterMonthEndReport
    {
        [Key]
        public int Id { get; set; }

        public int WcPrimaryId { get; set; }
        [JsonIgnore]
        public virtual WaterConnection? WaterConnection { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        [Precision(18, 2)]
        public decimal? LastYearArrears { get; set; }

        [Precision(18, 2)]
        public decimal? LYABalanceVAT { get; set; } = 0;

        [Precision(18, 2)]
        public decimal? ThisYearArrears { get; set; }
        [Precision(18, 2)]
        public decimal? TYABalanceVAT { get; set; }


        [Precision(18, 2)]
        public decimal? TMCharge { get; set; } = 0;
        [Precision(18, 2)]
        public decimal? TMBalanceVAT { get; set; } = 0;
        [Precision(18, 2)]
        public decimal? RemainOverPay { get; set; } = 0;
        [Precision(18, 2)]
        public decimal? RemainOverPayVat { get; set; } = 0;

        [Precision(18, 2)]
        public decimal? ReceivedOverPay { get; set; } = 0;
        [Precision(18, 2)]
        public decimal? ReceivedOverPayVAT { get; set; } = 0;



        [Precision(18, 2)]
        public decimal? LYArrearsPaying { get; set; } 

        [Precision(18, 2)]
        public decimal? TYArrearsPaying { get; set; }

        [Precision(18, 2)]
        public decimal? TMPaying { get; set; }

        [Precision(18, 2)]
        public decimal? OverPaying{ get; set; }

        [Precision(18, 2)]
        public decimal? OverPaymentWithVat { get; set; }

        [Precision(18, 2)]
        public decimal? MonthlyBill { get; set; }

        [Precision(18, 2)]
        public decimal? MonthlyBillWithVat { get; set; }



        // mandatory fields

        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


    }
}
