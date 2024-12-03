
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax
{
    public partial class AssessmentTempPartner
    {
        [Key]
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? NICNumber { get; set; }
        public string? MobileNumber { get; set; }
        //public string? PhoneNumber { get; set; }
        public string? Street1 { get; set; }
        public string? Street2 { get; set; }
        //public string? City { get; set; }
        //public string? Zip { get; set; }
        public string? Email { get; set; }
        [Required]
        public int? AssessmentId { get; set; }

        [JsonIgnore]
        public virtual Assessment? Assessment { get; set; }


        // mandatory fields
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public object RestClone(int assessmentId)
        {
            return new AssessmentTempPartner
            {
                Id = null,
                Name = Name,
                NICNumber = NICNumber,
                MobileNumber = MobileNumber,
                Street1 = Street1,
                Street2 = Street2,
                Email = Email,
                AssessmentId = assessmentId,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                CreatedBy = CreatedBy,
                UpdatedBy = UpdatedBy

            };
        }
    }
}
