using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.ShopRental
{
    public partial class ShopRentalProcess
    {
        [Key]
        public int? Id { get; set; }
        public int? ActionBy { get; set; }
        public DateOnly? Date { get; set; }
        //public int Year { get; set; }    //Balance -> Year|Month|Session Day
        //public int Month { get; set; }
        //public int Day { get; set; }
        public int ShabaId { get; set; }
        public ShopRentalProcessType ProcessType { get; set; }
        public DateTime? ProceedDate { get; set; }
        public string? BackUpKey { get; set; }

        public int? ProcessConfigId { get; set; }



        //---------------------------------
        [NotMapped]
        public int CurrentSessionMonth { get; set; }

        [NotMapped]
        public int CurrentSessionDay { get; set; } //1,2,3 ...

        //[NotMapped]
        //public int LastDayOfLastEndedSessionMonth { get; set; } //1,2,3 ...

        //[NotMapped]
        //public int? ProcessConfigId { get; set; }
        //---------------------------------

        public int? IsSkippeed { get; set; }
        
        public string? Description { get; set; }
        public virtual ShopRentalProcessConfigaration? ShopRentalProcessConfigaration { get; set; }

    }
}
