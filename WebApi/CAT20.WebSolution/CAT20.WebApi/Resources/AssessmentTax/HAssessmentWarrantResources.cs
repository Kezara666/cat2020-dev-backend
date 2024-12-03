namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class HAssessmentWarrantResources
    {

        public int SabhaId { get; set; }
        public int ActionBy { get; set; }
        public int WarrantMethod { get; set; }
        public int Quarter { get; set; }
        public List<int>? AssessmentIdList { get; set; }


    }
}
