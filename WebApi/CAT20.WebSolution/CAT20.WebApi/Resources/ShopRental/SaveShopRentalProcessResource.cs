namespace CAT20.WebApi.Resources.ShopRental
{
    public class SaveShopRentalProcessResource
    {
        public int Id { get; set; }
        public int ActionBy { get; set; }
        public int ShabaId { get; set; }


        //---------------------------------
        public int CurrentSessionMonth { get; set; }

        public int CurrentSessionDay { get; set; } //1,2,3 ...

        //public int LastDayOfLastEndedSessionMonth { get; set; } //1,2,3 ...

        public int? ProcessConfigId { get; set; }
        public int? IsSkippeed { get; set; }

        public string? Description { get; set; }
        //---------------------------------
    }
}
