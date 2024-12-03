using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class VoucherSubLine
    {
        [Key]
        public int? Id { get; set; }
        public int? VoucherLineId { get; set; }

        public string? Description { get; set; }

        [Precision(18, 2)]
        public decimal NetAmount { get; set; }
        [Precision(18, 2)]
        public decimal VAT { get; set; }
        [Precision(18, 2)]
        public decimal NBT { get; set; }

        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }


        [JsonIgnore]
        public virtual VoucherLine? VoucherLine { get; set; }


        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }



    }
}
