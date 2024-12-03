
namespace CAT20.Core.Models.Enums
{
    public enum OrderStatus
    {
        Deleted = 0,
        Draft = 1,
        Paid = 2,
        Posted = 3,
        Cross = 7,
        Cancel_Pending = 4,
        Cancel_Approved = 5,
        Cancel_Disapproved = 6,
        Journal_Entry_Pending_Approval = 10,
        Rejected_Journal_Entry = 11,
    }
}
