using CAT20.Core.Models.WaterBilling;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.WaterBilling
{
    public class WaterProjectMainRoadResource
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public int? SabhaId { get; set; }
        [JsonIgnore]
        public virtual ICollection<WaterProjectSubRoadResource>? SubRoads { get; set; }

        //// mandatory fields
        public int? Status { get; set; }
        ////public DateTime? CreatedAt { get; set; }
        ////public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
