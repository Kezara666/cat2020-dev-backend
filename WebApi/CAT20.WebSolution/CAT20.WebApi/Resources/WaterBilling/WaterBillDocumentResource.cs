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
    public class WaterBillDocumentResource
    {
    
        public int? Id { get; set; }
       
        public string? DocType { get; set; }
    
        public string? Uri { get;}
       
        [JsonIgnore]
        [NotMapped]
        public IFormFile? File { get; set; }

        public int? ConnectionId { get; set; }
        [Required]
        public string? ApplicationNo { get; set; }
        // mandatory fields
        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


    }
}
