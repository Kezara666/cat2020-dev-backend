using CAT20.Core.Models;
using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.User
{
    public class UserDetailBasicResource
    {
        public int ID { get; set; }
        public string NameInFull { get; set; }
        public string NameWithInitials { get; set; }
        public string Username { get; set; }
        public string NIC { get; set; }
        public string ContactNo { get; set; }
        public DateTime? Birthday { get; set; }
        public int? SabhaID { get; set; }
        public int? OfficeID { get; set; }
        public int? ActiveStatus { get; set; }
        public int? GenderID { get; set; }
        public Office Office { get; set; }

        public int? UserGroupID { get; set; }
    }
}
