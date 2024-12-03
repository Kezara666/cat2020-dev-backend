using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.WaterBilling
{
    public class WaterBillDocument
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public string? DocType { get; set; }

        [Required]
        public string? Uri { get; set; }
        
        [Required]
        [JsonIgnore]
        [NotMapped]
        public IFormFile? File { get; set; }

        //[Required]
        public int? ConnectionId { get; set; }

        [JsonIgnore]
        public virtual WaterConnection? WaterConnection { get; set; }


        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


    }
}
