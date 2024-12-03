namespace CAT20.Core.HelperModels
{
    public class HAssessmentWarrant
    {
        public int SabhaId { get; set; }
        public int ActionBy { get; set; }
        public int WarrantMethod { get; set; }
        public int Quarter { get; set; }
        public List<int>? AssessmentIdList { get; set; }
    }
}
