using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.WaterBilling;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.AssessmentTax;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorrectionController : ControllerBase
    {
        private readonly IAssessmentQuarterReportService _assessmentQuarterReport;
        private readonly IWaterMonthEndReportService _waterMonthEndReportService;

        public CorrectionController(IAssessmentQuarterReportService assessmentQuarterReport, IWaterMonthEndReportService waterMonthEndReportService)
        {
            _assessmentQuarterReport = assessmentQuarterReport;
            _waterMonthEndReportService = waterMonthEndReportService;
        }


        //[HttpPost("initialReportGenerate")]
        //public async Task<IActionResult> initialReportGenerate(List<int> includeIds)
        //{
        //    var x = await _assessmentQuarterReport.CreateInitialReport(includeIds);


        //    if(x.Item1)
        //    {
        //        return Ok(x);
        //    }
        //    else
        //    {
        //        return BadRequest(x);
        //    }
        //}

        //[HttpPost("Q1ReportGenerate")]
        //public async Task<IActionResult> Q1ReportGenerate(List<int> includeIds)
        //{
        //    var x = await _assessmentQuarterReport.CreateReportQ1(includeIds);
        //    if (x.Item1)
        //    {
        //        return Ok(x);
        //    }
        //    else
        //    {
        //        return BadRequest(x);
        //    }
        //}


        //[HttpPost("Q2ReportGenerate")]
        //public async Task<IActionResult> Q1ReportGenerate(List<int> includeIds)
        //{
        //    var x = await _assessmentQuarterReport.CreateReportQ2(includeIds);
        //    if (x.Item1)
        //    {
        //        return Ok(x);
        //    }
        //    else
        //    {
        //        return BadRequest(x);
        //    }
        //}




        //[HttpPost("openBill")]
        //public async Task<IActionResult> oldWaterReportGenerate()
        //{
        //    if (await _waterMonthEndReportService.CreateInitilReport(0))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpPost("createMonthlyReport")]
        //public async Task<IActionResult> CreateMonthlyReport()
        //{
        //    if (await _waterMonthEndReportService.CreateMonthlyReport(0))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpPost("validate")]
        //public async Task<IActionResult> Validate()
        //{
        //    if (await _waterMonthEndReportService.validate(0))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
