using CAT20.Core.Models.WaterBilling;
using CAT20.WebApi.Resources.User;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.WaterBilling
{
    public class WaterConnectionStatusLogResource
    {

        public int? Id { get; set; }

        public int? ConnectionStatus { get; set; }

        public int? ConnectionId { get; set; }

        public string? Comment { get; set; }
        public int? ActionBy { get; set; }
        public UserActionByResources? UserActionBy { get; set; }
        public string? ActionNote { get; set; }
        public DateTime? ActivatedDate { get; set; }

        //public virtual WaterConnectionResource? WaterConnection { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
