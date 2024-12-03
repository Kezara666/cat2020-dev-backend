using CAT20.Core.Models.HRM.PersonalFile;

namespace CAT20.WebApi.Resources.HRM.PersonalFile
{
    public class ChildInfoResource
    {
        public int? Id { get; set; }
        public int? EmployeeID { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }

    }
}
