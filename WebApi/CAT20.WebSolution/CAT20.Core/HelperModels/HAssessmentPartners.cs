// Ignore Spelling: Nic Nics

namespace CAT20.Core.HelperModels
{
    public class HAssessmentPartners
    {
        public List<HPartnerNIC>? PartnerNics { get; set; }
        public List<HPartnerName>? PartnerNames { get; set; }
    }


    //public class HPartnerNIC
    //{
    //    public int ModelKey { get; set; }

    //    public List<string>? Nics { get; set; }
    //}


    //public class HPartnerName
    //{
    //    public int ModelKey { get; set; }
    //    public List<string>? Names { get; set; }
    //}

    public class HPartnerNIC
    {
        public int ModelKey { get; set; }
        public int? Id { get; set; }
        public string? Nic { get; set; }
    }


    public class HPartnerName
    {
        public int ModelKey { get; set; }
        public int? Id { get; set; }
        public string? Name { get; set; }

    }
}
