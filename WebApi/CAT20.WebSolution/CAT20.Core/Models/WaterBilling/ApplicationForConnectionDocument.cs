using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CAT20.Core.Models.WaterBilling
{
    public class ApplicationForConnectionDocument
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

        [MaxLength(10)]
        [Required]
        public string? ApplicationNo { get; set; }

        [JsonIgnore]
        public virtual ApplicationForConnection? ApplicationForConnection { get; set; }


        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
