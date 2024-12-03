using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveVoucherInvoice
    {
        public int VoucherId { get; set; }
        public int Description { get; set; }
        public IFormFile? File { get; set; }
    }
}
