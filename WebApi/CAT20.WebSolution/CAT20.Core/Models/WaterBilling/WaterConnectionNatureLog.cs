using CAT20.Core.Models.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.WaterBilling
{
    public class WaterConnectionNatureLog
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int NatureId { get; set; }

        [JsonIgnore]
        public virtual WaterProjectNature? Nature { get; set; }

        [StringLength(150)]
        public string? Comment { get; set; }
        public WbAuditLogAction? Action { get; set; }
        public int? ActionBy { get; set; }

        [StringLength(150)]
        public string? ActionNote { get; set; }

        public DateTime? ActivatedDate { get; set; }

        [Required]
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
