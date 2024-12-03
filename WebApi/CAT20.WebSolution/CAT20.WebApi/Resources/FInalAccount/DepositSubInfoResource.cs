using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.FInalAccount
{
    public class DepositSubInfoResource
    {
        public int? Id { get; set; }
        public int? SabhaId { get; set; }
        public string Name { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


    }
}
