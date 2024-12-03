using CAT20.Core.Models.Control;

namespace CAT20.WebApi.Resources.User
{
    public class UserActionByResources
    {
        public int ID { get; set; }
        public string NameInFull { get; set; }
        public string NameWithInitials { get; set; }
        public string Username { get; set; }
        public string ContactNo { get; set; }
        public int? SabhaID { get; set; }
        public int? OfficeID { get; set; }
        public int? ActiveStatus { get; set; }
        public Office Office { get; set; }

        public int? UserGroupID { get; set; }
    }
}
