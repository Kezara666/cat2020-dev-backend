using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.ShopRental
{
    public  class DailyFineProcessLog
    {
        public int? Id {  get; set; }

        public int NoOfDates { get; set; }

        public decimal? DailyFineRate { get; set; }

        public decimal? DailyFixedFineAmount { get; set; }

        public decimal TotalFineAmount { get; set; }

        public int ShopId { get; set; }

        public int ProcessConfigurationId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public int BalanceId { get; set; }
  

        
    }
}
