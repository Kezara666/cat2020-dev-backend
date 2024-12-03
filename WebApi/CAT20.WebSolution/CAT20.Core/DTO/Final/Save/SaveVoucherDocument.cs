using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveVoucherDocument
    {
        public int VoucherId { get; set; }
        public string? DocType { get; set; }
        public IFormFile? File { get; set; }
    }
}
