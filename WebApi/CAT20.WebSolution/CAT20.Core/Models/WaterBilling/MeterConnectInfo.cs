using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.WaterBilling
{

    public class MeterConnectInfo
    {


        [Key]
        [MaxLength(20)]
        public string? ConnectionId { get; set; }
        [Required]
        [StringLength(50)]
        public string? ConnectionNo { get; set; }
        [Required]
        [StringLength(50)]
        public string MeterNo { get; set; }

        [JsonIgnore]
        public virtual WaterProjectSubRoad? WaterProjectSubRoad { get; set; }
        [Required]
        public int? SubRoadId { get; set; } // Foreign key

        public bool? IsAssigned { get; set; }

        [Required]
        public int? OrderNo { get; set; }

        [JsonIgnore]
        public virtual WaterConnection? WaterConnection { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }




        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var key = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());

            return key;
        }
    }
}
