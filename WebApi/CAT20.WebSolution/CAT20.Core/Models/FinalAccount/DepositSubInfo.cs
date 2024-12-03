using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.FinalAccount
{

    public class DepositSubInfo
    {
        public int Id { get; set; }
        public int SabhaId { get; set; }
        public string Name { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


        [Required]
        public DateTime? SystemCreateAt { get; set; }

        public DateTime? SystemUpdateAt { get; set; }
    }
}