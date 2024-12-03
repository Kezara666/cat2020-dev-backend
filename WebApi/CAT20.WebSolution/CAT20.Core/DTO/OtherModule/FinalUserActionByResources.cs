using CAT20.Core.Models.Control;

namespace CAT20.Core.DTO.OtherModule
{
    public class FinalUserActionByResources
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
