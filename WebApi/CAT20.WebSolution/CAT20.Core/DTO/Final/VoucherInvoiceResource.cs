using CAT20.Core.Models.FinalAccount;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class VoucherInvoiceResource
    {
        public int? Id { get; set; }
        public int? VoucherId { get; set; }
        public string? Description { get; set; }

        public string Uri { get; set; }

        //[Required]
        //[JsonIgnore]
        //[NotMapped]
        //public IFormFile? File { get; set; }

        //public virtual Voucher? Voucher { get; set; }

        //public int Status { get; set; }

        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }
    }
}
