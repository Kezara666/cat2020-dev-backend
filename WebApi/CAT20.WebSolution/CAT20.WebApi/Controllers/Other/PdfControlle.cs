using CAT20.WebApi.Controllers.Control;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CAT20.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFController : ControllerBase
    {
        //private IReportService _reportService;
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;
        private readonly HtmlToPdfService _htmlToPdfService;

        public PDFController(IWebHostEnvironment webHostEnvirnoment, HtmlToPdfService htmlToPdfService)
        {
            this._webHostEnvirnoment = webHostEnvirnoment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            _htmlToPdfService = htmlToPdfService;
        }


        //[HttpPost("generate-from-url")]
        //public async Task<IActionResult> GeneratePdfFromUrl([FromBody] string url)
        //{
        //    if (string.IsNullOrEmpty(url))
        //    {
        //        return BadRequest("URL is required.");
        //    }

        //    try
        //    {
        //        var pdfBytes = await _htmlToPdfService.ConvertUrlToPdfAsync(url);
        //        return File(pdfBytes, "application/pdf", "report.pdf");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}

        [HttpPost]
        [Route("convert-html-to-pdf")]
        public async Task<IActionResult> ConvertHtmlToPdf([FromBody] HtmlRequest request)
        {
            if (string.IsNullOrEmpty(request.HtmlContent))
            {
                return BadRequest(new { error = "The HTML content field is required." });
            }

            var pdfService = new HtmlToPdfService();
            var pdfBytes = await pdfService.ConvertHtmlToPdfAsync(request.HtmlContent);
            return File(pdfBytes, "application/pdf", "report.pdf");
        }

        public class HtmlRequest
        {
            public string HtmlContent { get; set; }
        }

    }
}
