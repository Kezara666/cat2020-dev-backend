namespace CAT20.WebApi.Resources.WaterBilling
{
    public class ProcessBillRequest
    {

        public int Year { get; set; }
        public int Month { get; set; }

        public int SubRoadId { get; set; }

        public int UserId { get; set; }

    }
}
