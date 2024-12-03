using CAT20.WebApi.Resources.User;

namespace CAT20.WebApi.Resources.ShopRental
{
    public class ShopRentalProcessResource
    {
        public int Id { get; set; }
        public int ActionBy { get; set; }
        public DateOnly? Date { get; set; }
        //public int Year { get; set; }    //Balance -> Year|Month|Session Day
        //public int Month { get; set; }
        //public int Day { get; set; }}
        public int ShabaId { get; set; }
        public string? ProcessType { get; set; }
        public DateTime? ProceedDate { get; set; }
        public UserActionByResources? UserActionBy { get; set; }



        //---------------------------------
        public int CurrentSessionMonth { get; set; }
        public int CurrentSessionDay { get; set; } //1,2,3 ...
        public int? ProcessConfigId { get; set; }
        //---------------------------------
    }
}
