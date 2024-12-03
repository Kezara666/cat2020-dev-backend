
namespace CAT20.Core.Models.Enums
{
    public enum FinalAccountActionStates
    {
        Deleted = 0,
        Init = 1,
        Edited = -1,

        CCApproved = 2,
        CCRejected = -2,


        Recommendation = 3,
        Discouragement = -3,

        Approval = 4,
        Disapproval = -4,

        Certification = 5,
        Decertification = -5,


        HasCheque = 6,
        FinalPaid = 7,

        Print = 8,
      
        ChequeRejected=9,

        OpenSettlement = 10,
        Settled = 11,

    }
}
