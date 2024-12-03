using CAT20.Core.Models.WaterBilling;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CAT20.WebApi.Resources.WaterBilling
{
    public class ApplicationForConnectionDocumentResource
    {

        public int? Id { get; set; }
        
        public string? DocType { get; set; }

        public string? Uri { get; set; }

        [JsonIgnore]
        [NotMapped]
        public IFormFile? File { get; set; }

        [Required]
        public string? ApplicationNo { get; set; }

        //public virtual ApplicationForConnection? ApplicationForConnection { get; set; }


        // mandatory fields
        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
