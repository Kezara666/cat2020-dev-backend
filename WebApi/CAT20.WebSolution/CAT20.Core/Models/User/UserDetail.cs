using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.User
{
    public partial class UserDetail
    {
        public UserDetail()
        {
        }

        public int ID { get; set; }
        public string NameInFull { get; set; }
        public string NameWithInitials { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string? Pin { get; set; }
        public string NIC { get; set; }
        public string ContactNo { get; set; }
        public DateTime? Birthday { get; set; }
        public int? SabhaID { get; set; }
        public int? OfficeID { get; set; }
        public int? ActiveStatus { get; set; }
        public int? GenderID { get; set; }
        public string? ProfilePicPath { get; set; }
        public string? UserSignPath { get; set; }

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
        public int IsAdmin { get; set; }
    }
}