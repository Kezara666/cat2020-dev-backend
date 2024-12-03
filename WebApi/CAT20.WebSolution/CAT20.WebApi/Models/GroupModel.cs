using CAT20.WebApi.Helpers;
using CAT20.Core.Models.User;

namespace CAT20.WebApi.Models
{
    public class GroupModel
    {
        public string id { get; set; }
        public string dateCreated { get; set; }
        public string description { get; set; }
        public bool isActive { get; set; }
        public List<SelectObjectModel> assignedList { get; set; }
        public List<SelectObjectModel> notAssignedList { get; set; }

        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator Group(GroupModel e)
        {
            return new Group()
            {
                ID = Utility.ParseInt(e.id),
                Description = e.description,
                IsActive = e.isActive,
                DateCreated = !string.IsNullOrEmpty(e.dateCreated) ? DateTime.Parse(e.dateCreated) : DateTime.Now,
                //TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8]
            };
        }

        public static explicit operator GroupModel(Group e)
        {
            return new GroupModel()
            {
                id = e.ID.Value.ToString(),
                description = e.Description,
                isActive = e.IsActive,
                dateCreated = e.DateCreated != null ? e.DateCreated.Value.ToString() : string.Empty,
                //timeStamp = Utility.TimeStampToString(e.TimeStamp)
            };
        }
    }
}
