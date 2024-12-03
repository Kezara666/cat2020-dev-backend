using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Policy;


using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using CAT20.WebApi.Controllers.Control;

namespace CAT20.WebApi.Controllers.ReportViewer
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReportViewerController : BaseController
    {
        private IConfiguration configuration;

        public ReportViewerController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        //---------------- [Start : get pdf report] --------------------
        [HttpPost]
        [Route("getPdfReport")]
        public IActionResult GetPdfReport([FromBody] ReportJsonObject request)
        {
            try
            {   //__format=pdf
                if (request.RepoType != null || request.RepoParameters != null)
                {
                    // Access the properties of request
                    string reportType = request.RepoType;
                    string reportParameters = request.RepoParameters;

                    //Get the birtReportServer from appsetting.json
                    string birtReportServer = configuration.GetValue<string>("ReportServerSettings:BirtReportServerUrl");

                    //---- Report Name -----
                    string reportDate = DateTime.Now.ToString("yyyy-MM-dd");
                    string reportOutPutName = reportType + "-" + "(" + reportDate + ")";

                    string reportURL = birtReportServer + "/birt/output?__report=" + reportType + reportParameters;

                    using (WebClient client = new WebClient())
                    {
                        // Download the document bytes
                        byte[] pdfByteArray = client.DownloadData(reportURL);

                        // Create a MemoryStream to store the PDF
                        using (MemoryStream pdfStream = new MemoryStream(pdfByteArray))
                        {
                            // Return the PDF as a file
                            return File(pdfStream.ToArray(), "application/pdf", reportOutPutName);
                        }
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        //---------------- [End : get pdf report] ----------------------



        //---------------- [Start : get docToPdfAsposeConvertedReport (using converter)]--------
        //[HttpPost]
        //[Route("getDocToPdfAsposeConvertedReport")]
        //public IActionResult GetDocToPdfAsposeConvertedReport([FromBody] ReportJsonObject request)
        //{
        //    try
        //    {
        //        //__format=doc
        //        if (request.RepoType != null || request.RepoParameters != null)
        //        {
        //            // Access the properties of request
        //            string reportType = request.RepoType;
        //            string reportParameters = request.RepoParameters;

        //            //Get the birtReportServer from appsetting.json
        //            string birtReportServer = configuration.GetValue<string>("ReportServerSettings:BirtReportServerUrl");

        //            //---- Report Name -----
        //            string reportDate = DateTime.Now.ToString("yyyy-MM-dd");
        //            string reportOutPutName = reportType + "-" + "(" + reportDate + ")";

        //            string reportURL = birtReportServer + "/birt/output?__report=" + reportType + reportParameters;

        //            using (WebClient client = new WebClient())
        //            {
        //                // Download the document bytes
        //                byte[] docByteArray = client.DownloadData(reportURL);

        //                // Create a MemoryStream to store the doc
        //                using (MemoryStream docStream = new MemoryStream(docByteArray))
        //                {
        //                    Document doc = new Document(docStream);

        //                    // Create a MemoryStream to store the converted PDF
        //                    using (MemoryStream pdfStream = new MemoryStream())
        //                    {
        //                        // Save the document as PDF
        //                        doc.Save(pdfStream, SaveFormat.Pdf);

        //                        // Rewind the MemoryStream to the beginning
        //                        pdfStream.Seek(0, SeekOrigin.Begin);

        //                        // Return the PDF as a file
        //                        return File(pdfStream.ToArray(), "application/pdf", reportOutPutName);
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch
        //    {

        //        return BadRequest();
        //    }
        //}
        //---------------- [End : get docToPdfAsposeConvertedReport (using converter)]--------



        //---------------- [Start : get html content] --------------------
        [HttpPost]
        [Route("getReportHtmlContent")]
        public IActionResult GetReportHtmllContent([FromBody] ReportJsonObject request)
        {
            try
            {
                //__format=html
                if (request.RepoType != null || request.RepoParameters != null)
                {
                    // Access the properties of request
                    string reportType = request.RepoType;
                    string reportParameters = request.RepoParameters;
                    string middlestrings = ".rptdesign&__format=html&__svg=true&__locale=en_US&__timezone=IST&__masterpage=true&__rtl=false&__cubememsize=10&&__pageoverflow=0&__overwrite=false&";

                    //Get the birtReportServer from appsetting.json
                    string birtReportServer = configuration.GetValue<string>("ReportServerSettings:BirtReportServerUrl");

                    string reportURL = birtReportServer + "/birt/output?__report=" + reportType + middlestrings + reportParameters;

                    //---- Report Name -----
                    string reportDate = DateTime.Now.ToString("yyyy-MM-dd");
                    string reportOutPutName = reportType + "-" + "(" + reportDate + ")" + ".pdf";


                    using (WebClient client = new WebClient())
                    {
                        // Download the html bytes
                        byte[] htmlByteArray = client.DownloadData(reportURL);

                        // Create a MemoryStream to store html bytes
                        using (MemoryStream htmlStream = new MemoryStream(htmlByteArray))
                        {
                            // Return the html as a file
                            return File(htmlStream.ToArray(), "text/html", reportOutPutName);
                        }
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        //---------------- [End : get html content] ----------------------



        //---------------- [Start : get xml content] --------------------
        [HttpPost]
        [Route("getReportXlsContent")]
        public IActionResult GetReportXlsContent([FromBody] ReportJsonObject request)
        {
            try
            {
                //__format=xml
                if (request.RepoType != null || request.RepoParameters != null)
                {
                    // Access the properties of request
                    string reportType = request.RepoType;
                    string reportParameters = request.RepoParameters;

                    //Get the birtReportServer from appsetting.json
                    string birtReportServer = configuration.GetValue<string>("ReportServerSettings:BirtReportServerUrl");

                    string reportURL = birtReportServer + "/birt/output?__report=" + reportType + reportParameters;

                    //---- Report Name -----
                    string reportDate = DateTime.Now.ToString("yyyy-MM-dd");
                    string reportOutPutName = reportType + "-" + "(" + reportDate + ")" + ".xls";


                    using (WebClient client = new WebClient())
                    {
                        // Download the xml bytes
                        byte[] xlsByteArray = client.DownloadData(reportURL);

                        return File(xlsByteArray.ToArray(), "application/vnd.ms-excel", reportOutPutName);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        //---------------- [End : get html content] ----------------------


        [HttpPost]
        [Route("getReportHtmlString")]
        public async Task<IActionResult> GetReportHtmlString([FromBody] ReportJsonObject request)
        {
            {
                try
                {
                    //__format=html
                    if (request.RepoType != null || request.RepoParameters != null)
                    {
                        // Access the properties of request
                        string reportType = request.RepoType;
                        string reportParameters = request.RepoParameters;
                        string middlestrings = ".rptdesign&__format=html&__svg=true&__locale=en_US&__timezone=IST&__masterpage=true&__rtl=false&__cubememsize=10&&__pageoverflow=0&__overwrite=false&";

                        //Get the birtReportServer from appsetting.json
                        string birtReportServer = configuration.GetValue<string>("ReportServerSettings:BirtReportServerUrl");

                        string reportURL = birtReportServer + "/birt/output?__report=" + reportType + middlestrings + reportParameters;

                        using (HttpClient client = new HttpClient())
                        {
                            HttpResponseMessage response = await client.GetAsync(reportURL);
                            if (response.IsSuccessStatusCode)
                            {
                                string htmlContent = await response.Content.ReadAsStringAsync();
                                return Ok(Content(htmlContent, "text/html"));
                            }
                            else
                            {
                                return BadRequest("Failed to retrieve report content");
                            }
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch
                {
                    return BadRequest();
                }
            }
        }

    }

    public class ReportJsonObject
    {
        public string? RepoType { get; set; }

        public string? RepoParameters { get; set; }

        public int? PrimaryId { get; set; }
    }
}
