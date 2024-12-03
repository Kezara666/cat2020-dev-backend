using CAT20.Core.Models.HRM.PersonalFile;

namespace CAT20.WebApi.Resources.HRM.PersonalFile
{
    public partial class SalaryStructureDataResource
    {
        public int Id { get; set; }
        public string ServiceCategory { get; set; }
        public string SalaryCode { get; set; }
        public decimal? InitialStep { get; set; }
        public int? Years1 { get; set; }
        public decimal? FirstSlab { get; set; }
        public int? Years2 { get; set; }
        public decimal? SecondSlab { get; set; }
        public int? Years3 { get; set; }
        public decimal? ThirdSlab { get; set; }
        public int? Years4 { get; set; }
        public decimal? FourthSlab { get; set; }
        public decimal? Maximum { get; set; }

    }
}
