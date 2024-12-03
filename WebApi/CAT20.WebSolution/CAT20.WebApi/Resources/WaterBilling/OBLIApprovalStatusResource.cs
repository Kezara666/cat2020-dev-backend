using CAT20.Core.Models.WaterBilling;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.WaterBilling
{
    public class OBLIApprovalStatusResource
    {

        public int? Id { get; set; }
        public int? OpenBalStatus { get; set; }
        public int? OpnBalInfoId { get; set; }
        //public virtual OpeningBalanceInformation? OpeningBalanceInformation { get; set; }
        public string? Comment { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
    }
}
