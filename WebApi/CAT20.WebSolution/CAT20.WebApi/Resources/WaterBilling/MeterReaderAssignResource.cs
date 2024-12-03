using CAT20.Core.Models.WaterBilling;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.WaterBilling
{
    public class MeterReaderAssignResource
    {
        
        public int? Id { get; set; }

        public int? MeterReaderId { get; set; }
 
        public int? SubRoadId { get; set; }

        //public WaterProjectSubRoadResource? SubRoad { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
