using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAT20.WebApi.Resources.User
{
    public class UserDetailResource
    {
        public int ID { get; set; }
        public string NameInFull { get; set; }
        public string NameWithInitials { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NIC { get; set; }
        public string ContactNo { get; set; }
        public DateTime? Birthday { get; set; }
        public int? SabhaID { get; set; }
        public int? OfficeID { get; set; }
        public int? ActiveStatus { get; set; }
        public int? GenderID { get; set; }
        public string? ProfilePicPath { get; set; }
        public string? UserSignPath {  get; set; }

        [JsonIgnore]
        [NotMapped]
        public IFormFile? ProfilePic { get; set; }

        [JsonIgnore]
        [NotMapped]
        public IFormFile? UserSign { get; set; }
        public int? Q1Id { get; set; }
        public string? Answer1 { get; set; }
        public int? Q2Id { get; set; }
        public string? Answer2 { get; set; }
    }
}
