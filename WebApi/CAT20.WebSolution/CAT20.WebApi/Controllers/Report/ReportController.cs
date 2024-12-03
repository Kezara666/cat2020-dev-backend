
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CAT20.Services;
using CAT20.Core.Services.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using System.Text;
using Microsoft.AspNetCore.StaticFiles;
using System.Reflection;
using CAT20.Core.Repositories.Vote;
using System.IO;
using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;
using CoreHtmlToImage;
using Spire.Doc;
using System.Net;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.WaterBilling;

namespace CAT20.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : BaseController
    {
        //private IReportService _reportService;
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;

        public ReportController(IWebHostEnvironment webHostEnvirnoment)
        {
            //_reportService = reportService;
            this._webHostEnvirnoment = webHostEnvirnoment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }


        [HttpPost]
        [Route("getReceiptById/{orderid}")]
        public async Task<ActionResult> getReceiptById(int orderid)
        {
            try
            {
                if (orderid != null && orderid != 0)
                {
                    string reportType = "OrderReceipt";
                    string reportformat = "doc"; // word 2003 - 2007 .doc format
                                                 //string reportformat = "odt"; //Open Document Type
                                                 //string reportformat = "xls"; //Old Excel Format 
                                                 //string reportformat = "ppt"; //Power Point 
                    string mimetype = "";
                    if (reportformat == "doc")
                    {
                        mimetype = "application/msword";
                    }
                    else if (reportformat == "xls")
                    {
                        mimetype = "application/vnd.ms-excel";
                    }
                    else if (reportformat == "odt")
                    {
                        mimetype = "application/vnd.oasis.opendocument.text";
                    }
                    else if (reportformat == "ppt")
                    {
                        mimetype = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                    }
                    else
                    {
                        // Handle unexpected format
                        return BadRequest("Invalid report format");
                    }
                    string reportParameters = "mixinorderreceipt_eng.rptdesign&__format=" + reportformat + "&__svg=true&__locale=en_US&__timezone=IST&__masterpage=true&__rtl=false&__cubememsize=10&&__pageoverflow=0&__overwrite=false&orderid=" + orderid;
                    string birtReportServer = "https://cat2020.lk"; //configuration.GetValue<string>("ReportServerSettings:BirtReportServerUrl");
                    string reportURL = birtReportServer + "/birt/output?__report=" + reportParameters;
                    string reportDate = DateTime.Now.ToString("yyyy-MM-dd");
                    string reportOutPutName = reportType + "-" + reportDate + "_" + orderid + reportformat;

                    using (WebClient client = new WebClient())
                    {
                        // Download HTML content as a string
                        string htmlContent = client.DownloadString(reportURL);

                        // Now 'htmlContent' contains the HTML content from the specified URL
                        reportParameters = htmlContent;

                        byte[] ByteArrayFromBirtReport = client.DownloadData(reportURL);

                        using (MemoryStream fileStream = new MemoryStream(ByteArrayFromBirtReport))
                        {
                            return File(fileStream.ToArray(), mimetype, reportOutPutName);
                        }
                    }
                }
                else
                {
                    return BadRequest("Invalid order ID");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return BadRequest("An error occurred");
            }
        }

    //[HttpPost]
    //[Route("getReceiptById/{orderid}")]
    //public IActionResult getReceiptById(int orderid)
    //{
    //    try
    //    {
    //        if (orderid != null && orderid != 0)
    //        {
    //            string reportType = "OrderReceipt";
    //            string reportformat = "doc"; // word 2003 - 2007 .doc format
    //            //string reportformat = "odt"; //Open Document Type
    //            //string reportformat = "xls"; //Old Excel Format 
    //            //string reportformat = "ppt"; //Power Point 

    //            string mimetype = "";

    //            if (reportformat == "doc")
    //            {
    //                mimetype = "application/msword";
    //            }
    //            else if (reportformat == "xls")
    //            {
    //                mimetype = "application/vnd.ms-excel";
    //            }
    //            else if (reportformat == "odt")
    //            {
    //                mimetype = "application/vnd.oasis.opendocument.text";
    //            }
    //            else if (reportformat == "ppt")
    //            {
    //                mimetype = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
    //            }
    //            else
    //            {
    //                // Handle unexpected format
    //                return BadRequest("Invalid report format");
    //            }

    //            string reportParameters = "mixinorderreceipt_eng.rptdesign&__format=" + reportformat + "&__svg=true&__locale=en_US&__timezone=IST&__masterpage=true&__rtl=false&__cubememsize=10&&__pageoverflow=0&__overwrite=false&orderid=" + orderid;

    //            string birtReportServer = "https://cat2020.lk"; //configuration.GetValue<string>("ReportServerSettings:BirtReportServerUrl");
    //            string reportURL = birtReportServer + "/birt/output?__report=" + reportParameters;

    //            string reportDate = DateTime.Now.ToString("yyyy-MM-dd");
    //            string reportOutPutName = reportType + "-" + "(" + reportDate + ")." + reportformat;

    //            using (WebClient client = new WebClient())
    //            {
    //                byte[] ByteArrayFromBirtReport = client.DownloadData(reportURL);

    //                using (MemoryStream fileStream = new MemoryStream(ByteArrayFromBirtReport))
    //                {
    //                    return File(fileStream.ToArray(), mimetype, reportOutPutName);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            return BadRequest("Invalid order ID");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the exception for debugging purposes
    //        Console.WriteLine($"Exception: {ex.Message}");
    //        return BadRequest("An error occurred");
    //    }
    //}


    [HttpGet]
        [Route("getReceipt")]
        public IActionResult GetReceipt()
        {
            try
            {
                var converter = new HtmlConverter();
                var bytes = converter.FromUrl("https://cat2020.lk/birt/output?__report=mixinorderreceipt_sin.rptdesign&__format=html&__svg=true&__locale=en_US&__timezone=IST&__masterpage=true&__rtl=false&__cubememsize=10&&__pageoverflow=0&__overwrite=false&orderid=97180");
                //File.WriteAllBytes("image.jpg", bytes);
                MemoryStream ms = new MemoryStream(bytes);
                return new FileStreamResult(ms, "image/jpeg");
                //byte[] pdfBytes = null;
                //const string physicalPath = @"https://cat2020.lk/birt/output?__report=mixinorderreceipt_sin.rptdesign&__format=html&__svg=true&__locale=en_US&__timezone=IST&__masterpage=true&__rtl=false&__cubememsize=10&&__pageoverflow=0&__overwrite=false&orderid=97180";

                //using (var client = new System.Net.WebClient())
                //{
                //    pdfBytes = client.DownloadData(physicalPath);
                //}
                //MemoryStream ms = new MemoryStream(pdfBytes);
                //return new FileStreamResult(ms, "application/pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("sarapReceiptsDailyReport")]
        public IActionResult SarapReceiptsDailyReport()
        {
            try
            {
                byte[] pdfBytes = null;
                    //const string physicalPath = @"http://127.0.0.1:58336/viewer/frameset?__report=D%3A%5CGIT-Repository%5CReportModule%5CCAT20%5Csarapdailyreport.rptdesign&__format=pdf";
                //const string physicalPath = @"http://127.0.0.1:8080/birt/sarapdailyreport.rptdesign&__format=pdf";
                const string physicalPath = @"http://127.0.0.1:8080/birt/sarapdailyreport.rptdesign";

                using (var client = new System.Net.WebClient())
                    {
                        pdfBytes = client.DownloadData(physicalPath);
                    }
                MemoryStream ms = new MemoryStream(pdfBytes);
                return new FileStreamResult(ms, "application/pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getMixinSarapReceiptsDailyReport/{reportType}/{officeId}/{fordate}")]
        public async Task<ActionResult> MixinSarapReceiptsDailyReportForOffice(string reportType, int officeId, DateTime fordate)
        {
            try
            {
                byte[] pdfBytes = null;
                 //string physicalPath = "localhost:8080/birt/sarapdailyreport.rptdesign&__format=" + reportType;
                 string physicalPath = "https://cat2020.lk/birt/frameset?__report=sarapdailyreport.rptdesign&__format=" + reportType;
                using (var client = new System.Net.WebClient())
                {
                    pdfBytes = client.DownloadData(physicalPath);
                }
                string reportName = "MixinSarapReceiptsDailyReportForOffice";
                MemoryStream reportFileByteString = new MemoryStream(pdfBytes);
                return File(reportFileByteString, MediaTypeNames.Application.Octet, getReportName(reportName, reportType));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[HttpGet]
        //public IActionResult Report()
        //{
        //    string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("CAT20.WebApi.dll", string.Empty);
        //    var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), fileDirPath + "Reports\\Untitled.frx");
        //    path = path.Replace("\\", "/");

        //    var webReport = new FastReport.Report();
        //    //var mssqlDataConnection = new MsSqlDataConnection();
        //    //mssqlDataConnection.ConnectionString = _configuration.GetConnectionString("NorthWindConnection");
        //    //webReport.Report.Dictionary.Connections.Add(mssqlDataConnection);
        //    webReport.Report.Load(path);
        //    //var categories = GetTable<Category>(_northwindContext.Categories, "Categories");
        //    //webReport.Report.RegisterData(categories, "Categories");
        //    //return Ok(webReport);
        //    // prepare report
        //    // prepare the report
        //     webReport.Prepare();

        //    PDFSimpleExport export = new PDFSimpleExport();
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        export.Export(webReport, ms);
        //        ms.Flush();
        //        return File(ms.ToArray(), "application/pdf");
        //    }


        //    //HTMLExport export = new HTMLExport();
        //    //export.Layers = true;
        //    //using (MemoryStream ms = new MemoryStream())
        //    //{
        //    //    export.EmbedPictures = true;
        //    //    export.Export(webReport, ms);
        //    //    ms.Flush();
        //    //    //Encoding.UTF8.GetString(ms.ToArray());
        //    //    return File(ms, "application/pdf");
        //    //}
        //    //return Ok();
        //}


        //[HttpGet]
        //public IActionResult CreatePDF()
        //{
        //    var globalSettings = new GlobalSettings
        //    {
        //        ColorMode = ColorMode.Color,
        //        Orientation = Orientation.Portrait,
        //        PaperSize = PaperKind.A4,
        //        Margins = new MarginSettings { Top = 10 },
        //        DocumentTitle = "PDF Report",
        //        Out = @"D:\PDFCreator\Employee_Report.pdf"
        //    };
        //    var objectSettings = new ObjectSettings
        //    {
        //        PagesCount = true,
        //        HtmlContent = TemplateGenerator.GetHTMLString(),
        //        WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
        //        HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
        //        FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
        //    };
        //    var pdf = new HtmlToPdfDocument()
        //    {
        //        GlobalSettings = globalSettings,
        //        Objects = { objectSettings }
        //    };
        //    _converter.Convert(pdf);
        //    return Ok("Successfully created PDF document.");
        //}

        //public static void Load(LocalReport report, decimal widgetPrice, decimal gizmoPrice)
        //{
        //    var items = new[] { new ReportItem { Description = "Widget 6000", Price = widgetPrice, Qty = 1 }, new ReportItem { Description = "Gizmo MAX", Price = gizmoPrice, Qty = 25 } };
        //    var parameters = new[] { new ReportParameter("Title", "Invoice 4/2020") };
        //    using var rs = Assembly.GetExecutingAssembly().GetManifestResourceStream("ReportViewerCore.Sample.AspNetCore.Reports.Report.rdlc");
        //    report.LoadReportDefinition(rs);
        //    report.DataSources.Add(new ReportDataSource("Items", items));
        //    report.SetParameters(parameters);
        //}

        //[HttpGet]
        //[Route("generateReport")]
        //public IActionResult GenerateReport()
        //{
        //    try
        //    {
        //        string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("CAT20.WebApi.dll", string.Empty);
        //        var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), fileDirPath + "Reports\\test.rdlc");
        //        path = path.Replace("\\", "/");
        //        using var fs = new FileStream(path, FileMode.Open);

        //        System.IO.Stream reportDefinition= fs; // your RDLC from file or resource
        //        //IEnumerable dataSource; // your datasource for the report

        //        LocalReport report = new LocalReport();
        //        report.LoadReportDefinition(reportDefinition);
        //        //report.DataSources.Add(new ReportDataSource("source", dataSource));
        //        //report.SetParameters(new[] { new ReportParameter("Parameter1", "Parameter value") });
        //        byte[] pdf = report.Render("PDF");

        //        return File(pdf, "application/pdf");
        //        ////Simple load and report execution and generation in a HTML Result file
        //        //Repository repository = Repository.Create();



        //        //Seal.Model.Report report = Seal.Model.Report.LoadFromFile(path, repository);
        //        //ReportExecution execution = new ReportExecution() { Report = report };
        //        //execution.Execute();
        //        //while (report.IsExecuting) System.Threading.Thread.Sleep(100);
        //        //string result = execution.GenerateHTMLResult();
        //        //Process.Start(result);
        //        //return Ok(result);

        //        //string renderFormat = "PDF";
        //        //string extension = "pdf";
        //        //string mimeType = "application/pdf";

        //        //decimal WidgetPrice = 104;
        //        //decimal GizmoPrice = 1;
        //        //using var report = new Microsoft.Reporting.NETCore.LocalReport();
        //        //Load(report, WidgetPrice, GizmoPrice);
        //        //var pdf = report.Render(renderFormat);
        //        //return File(pdf, mimeType, "report." + extension);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.Message);
        //    }
        //}


        //public static void Load(Microsoft.Reporting.NETCore.LocalReport report, decimal widgetPrice, decimal gizmoPrice)
        //{
        //    var items = new[] { new ReportItem { Description = "Widget 6000", Price = widgetPrice, Qty = 1 }, new ReportItem { Description = "Gizmo MAX", Price = gizmoPrice, Qty = 25 } };
        //    //var parameters = new[] { new ReportParameter("Title", "Invoice 4/2020") };

        //    using var rs = Assembly.GetExecutingAssembly().GetManifestResourceStream("CAT20.WebApi.Reports.test.rdlc");
        //    report.LoadReportDefinition(rs);
        //    //report.DataSources.Add(new ReportDataSource("Items", items));
        //    //report.SetParameters(parameters);
        //}


        //[HttpGet]
        //[Route("generatefastpdf")]
        //public IActionResult GetReportAsPdf()
        //{
        //    try { 
        //    ExportWithPdfSimple();
        //    ExportWithoutPdfSimple();

        //    return Ok("Done");
        //    }
        //    catch(Exception ex)
        //    {
        //        return BadRequest("Error : " + ex.Message) ;
        //    }
        //}

        //static void ExportWithPdfSimple()
        //{
        //    var data = GenerateData();
        //    var fastReportGenerator = new FastReportGenerator<TestReportDataModel>(FastReport.OpenSource.HtmlExporter.ReportUtils.DesignerPath, "test.frx");
        //    var report = fastReportGenerator.GenerateWithPDFSimplePlugin(data);
        //    ExportToFile(report, "testWithPdfSimple");
        //}

        //static void ExportWithoutPdfSimple()
        //{
        //    var data = GenerateData();
        //    var fastReportGenerator = new FastReportGenerator<TestReportDataModel>(FastReport.OpenSource.HtmlExporter.ReportUtils.DesignerPath, "test.frx");
        //    var report = fastReportGenerator.GeneratePdfFromHtml(data, PageSize.A4);
        //    ExportToFile(report, "testWithoutPdfSimple");
        //}

        //static void ExportToFile(byte[] report, string fileName)
        //{
        //    fileName = System.IO.Path.Combine(FastReport.OpenSource.HtmlExporter.ReportUtils.ExportPath, string.Format("{0}.pdf", fileName));
        //    if (System.IO.File.Exists(fileName))
        //    {
        //        System.IO.File.Delete(fileName);
        //    }
        //    System.IO.File.WriteAllBytes(fileName, report);
        //}

        //static List<TestReportDataModel> GenerateData()
        //{
        //    var data = new List<TestReportDataModel>();
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        data.Add(new TestReportDataModel { Id = i });
        //    }
        //    return data;
        //}

        //public IActionResult GetBarcode(string barcodeText)
        //{
        //    var font = new Font("Code 128", 20);

        //    using (var bitmap = new Bitmap(barcodeText.Length * 40, 100))
        //    {
        //        using (var graphics = Graphics.FromImage(bitmap))
        //        {
        //            graphics.Clear(Color.White);

        //            var textWidth = graphics.MeasureString(barcodeText, font).Width;
        //            graphics.DrawString(barcodeText, font, Brushes.Black, (bitmap.Width - textWidth) / 2, 0);

        //            var stream = new MemoryStream();
        //            bitmap.Save(stream, ImageFormat.Png);
        //            stream.Position = 0;

        //            return File(stream, "image/png");
        //        }
        //    }
        //}


        //[HttpGet]
        //[Route("pdf")]
        //public IActionResult GetReportAsPdf()
        //{
        //    // Load the report file
        //    var report = new Report();
        //    string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("CAT20.WebApi.dll", string.Empty);
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), fileDirPath + "Reports\\test.frx");
        //    report.Load(path);

        //    // Set report parameters and data sources as necessary

        //    // Export the report to PDF
        //    var pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport();
        //    var pdfStream = new MemoryStream();
        //    pdfExport.Export(report, pdfStream);
        //    pdfStream.Seek(0, SeekOrigin.Begin);

        //    // Return the PDF file as a response
        //    return File(pdfStream, "application/pdf", "report.pdf");
        //}

        //public static void LoadReport(LocalReport report)
        //    {
        //    //var items = new[] { new ReportItem { Description = "Widget 6000", Price = 104.99m, Qty = 1 }, new ReportItem { Description = "Gizmo MAX", Price = 1.41m, Qty = 25 } };
        //    //var parameters = new[] { new ReportParameter("Title", "Invoice 4/2020") };
        //    using var rs = Assembly.GetExecutingAssembly().GetManifestResourceStream("CAT20.WebApi.Reports.test.rdlc");
        //    //report.DataSources.Add(new ReportDataSource("Items", items));
        //    report.LoadReportDefinition(rs);
        //    //report.SetParameters(parameters);
        //    //using var fs = new FileStream("Report.rdlc", FileMode.Open);
        //    //report.LoadReportDefinition(fs);
        //}



        //public IActionResult RenderReport()
        //{
        //    // Create a new ReportViewer control
        //    var viewer = new ReportViewer();

        //    // Set the processing mode to remote
        //    viewer.ProcessingMode = ProcessingMode.Remote;

        //    // Set the URL of the report server
        //    viewer.ServerReport.ReportServerUrl = new Uri("http://reportserver/ReportServer");

        //    // Set the path of the RDLC report on the server
        //    viewer.ServerReport.ReportPath = "/MyReports/MyReport";

        //    // Set any parameters that the report requires
        //    var parameters = new ReportParameterCollection();
        //    parameters.Add(new ReportParameter("Parameter1", "Value1"));
        //    viewer.ServerReport.SetParameters(parameters);

        //    // Render the report
        //    byte[] reportBytes = viewer.ServerReport.Render("PDF");

        //    // Return the report as a file download
        //    return File(reportBytes, "application/pdf", "MyReport.pdf");
        //}


        //[HttpGet]
        //[Route("getMixinOrdersForOfficeReport/{reportType}/{officeId}")]
        //public async Task<ActionResult> MixinOrdersReportForOffice(string reportType, int officeId)
        //{
        //    string reportName = "MixinOrdersReportForOffice";
        //    var reportFileByteString = await _reportService.MixinOrdersReportForOfficeAsync(reportName, reportType, officeId);
        //    return File(reportFileByteString, MediaTypeNames.Application.Octet, getReportName(reportName, reportType));
        //}

        //[HttpGet]
        //[Route("getMixinSarapReceiptsDailyReport/{reportType}/{officeId}/{fordate}")]
        //public async Task<ActionResult> MixinSarapReceiptsDailyReportForOffice(string reportType, int officeId, DateTime fordate)
        //{
        //    try
        //    {
        //        string reportName = "MixinSarapReceiptsDailyReportForOffice";
        //        var reportFileByteString = await _reportService.MixinSarapReceiptsDailyReportForOfficeAsync(reportName, reportType, officeId, fordate);
        //        //return File(reportFileByteString, "application/pdf");
        //        return File(reportFileByteString, MediaTypeNames.Application.Octet, getReportName(reportName, reportType));
        //    }
        //    catch(Exception ex)
        //    {
        //        return Ok(ex.Message);
        //    }
        //}

        //[HttpGet]
        //[Route("Print")]
        //public IActionResult Print()
        //{
        //    string mimtype = "";
        //    int extension = 1;
        //    //var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\test.rdlc";
        //    string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("CAT20.WebApi.dll", string.Empty);
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), fileDirPath + "Reports\\test.pdf");
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    //parameters.Add("rp1", "ASP.NET CORE RDLC Report");
        //    //get products from product table 

        //    try
        //    {
        //        LocalReport localReport = new LocalReport(path);
        //        var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
        //        try
        //        {
        //            return File(result.MainStream, "application/pdf");
        //        }
        //        catch (Exception ex)
        //        {
        //            return Ok("Error in returning"+ ex.Message);
        //        }
        //    }

        //    catch(Exception ex)
        //    {
        //        return Ok("Error in Rendering" + ex.Message);
        //    }
        //}


        //[HttpGet]
        //[Route("PrintNew")]
        //public IActionResult PrintNew()
        //{
        //    try
        //    {
        //        string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("CAT20.WebApi.dll", string.Empty);
        //        var path = Path.Combine(Directory.GetCurrentDirectory(), fileDirPath + "Reports\\test.rdlc");

        //        FileStream reportStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        //        //inputStream.Close();

        //        return File(reportStream, "application/pdf");
        //        //string mimtype = "";
        //        //int extension = 1;
        //        ////var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\test.rdlc";
        //        //string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("CAT20.WebApi.dll", string.Empty);
        //        //var path = Path.Combine(Directory.GetCurrentDirectory(), fileDirPath + "Reports\\test.rdlc");
        //        //Dictionary<string, string> parameters = new Dictionary<string, string>();
        //        ////parameters.Add("rp1", "ASP.NET CORE RDLC Report");
        //        ////get products from product table 
        //        //LocalReport localReport = new LocalReport(path);
        //        //var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
        //        //return File(result.MainStream, "application/pdf");
        //    }

        //    catch (Exception ex)
        //    {
        //        return Ok(ex.Message);
        //    }
        //}
        //[HttpGet]
        //[Route("Report")]
        //public IActionResult Report()
        //{
        //    try
        //    {
        //        //var test = _context.Table.ToList();
        //        //string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("CAT20.WebApi.dll", string.Empty);
        //        //var path = Path.Combine(Directory.GetCurrentDirectory(), fileDirPath + "Reports\\test.rdlc");
        //        var path = $"{_webHostEnvirnoment.WebRootPath}Reports\\test.rdlc";
        //        LocalReport report = new LocalReport();
        //        report.ReportPath = path;

        //        //report.DataSources.Add(new ReportDataSource("DataSet1", test));
        //        //report.SetParameters(new[] { new ReportParameter("rdl", "Student Name") });
        //        byte[] pdf = report.Render("PDF");
        //        return File(pdf, "application/pdf");
        //    }

        //    catch (Exception ex)
        //    {
        //        return Ok(ex.Message);
        //    }
        //}
    


        //[HttpGet]
        //[Route("TestReport/{Type}")]
        //public IActionResult VerReporte(string Type)
        //{
        //    try { 
        //    string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("CAT20.WebApi.dll", string.Empty);
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), fileDirPath + "Reports\\test.rdlc");
        //    LocalReport lr = new LocalReport();
        //    string rutaReporte = Path.Combine(path);
        //    if (System.IO.File.Exists(rutaReporte))
        //    {
        //        lr.ReportPath = rutaReporte;
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }

        //    //List<Paises> pa = new List<Paises>();
        //    //using (DatosEntities d = new DatosEntities())
        //    //{

        //    //    pa = d.Paises.ToList();
        //    //}

        //    //ReportDataSource rd = new ReportDataSource("DataSet1", pa);
        //    //lr.DataSources.Add(rd);
        //    string reportType = Type;
        //    string mimeType;
        //    string encoding;
        //    string fileNameExtension;


        //    Warning[] warnings;
        //    string[] streams;
        //    byte[] renderBytes;

        //    string deviceInfo =
        //        "<DeviceInfo>" +
        //        " <OutputFormat>" + reportType + "</OutputFormat>" +
        //        " <PageWidth>8.5in</PageWidth>" +
        //        " <PageHeight>11in</PageHeight>" +
        //        " <MarginTop>0.5</MarginTop>" +
        //        " <MarginLeft>1in</MarginLeft>" +
        //        " <MarginRight>1in</MarginRight>" +
        //        " <MarginBottom>0.5in</MarginBottom>" +
        //        " </DeviceInfo>";

        //    renderBytes = lr.Render(
        //        reportType,
        //        deviceInfo,
        //        out mimeType,
        //        out encoding,
        //        out fileNameExtension,
        //        out streams,
        //        out warnings
        //        );

        //    return File(renderBytes, mimeType);
        //    }
        //    catch( Exception ex ) { return Ok(ex.Message) ; }
        //}


        //[HttpGet, DisableRequestSizeLimit]
        //[Route("GetMyPdf")]
        //public IActionResult GetMyPdf()
        //{
        //    string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("CAT20.WebApi.dll", string.Empty);
        //    var pdfPath = Path.Combine(Directory.GetCurrentDirectory(), fileDirPath+ "Reports\\test.pdf");
        //    byte[] bytes = System.IO.File.ReadAllBytes(pdfPath);
        //    return File(bytes, "application/pdf");
        //}


        [HttpGet("get-file-content")]
        public async Task<FileContentResult> DownloadAsync()
        {
            //var fileName = "myfileName.pdf";
            //var mimeType = "application/pdf";
            //DateTime fordate = DateTime.Today;
            //byte[] fileBytes = await _reportService.MixinSarapReceiptsDailyReportForOfficeAsync("MixinSarapReceiptsDailyReportForOffice", "pdf", 118, fordate);

            //return new FileContentResult(fileBytes, mimeType)
            //{
            //    FileDownloadName = fileName
            //};

            using (var ms = new System.IO.MemoryStream())
            {
                ms.WriteByte((byte)'a');
                ms.WriteByte((byte)'b');
                ms.WriteByte((byte)'c');
                byte[] data = ms.ToArray();
                return File(data, "application/pdf", "downloaded_file.pdf");
            }
        }

        [HttpGet]
        [Route("load-from-server-physical-location")]
        public IActionResult PhysicalLocation()
        {
            try
            {
                string physicalPath = "Files/ReportFiles/test.pdf";
                byte[] pdfBytes = System.IO.File.ReadAllBytes(physicalPath);
                MemoryStream ms = new MemoryStream(pdfBytes);
                return new FileStreamResult(ms, "application/pdf");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            
        }

        //[HttpGet("get-file-location")]
        //public async Task<String> getlocation()
        //{
        //    var fileName = "myfileName.pdf";
        //    var mimeType = "application/pdf";
        //    DateTime fordate = DateTime.Today;
        //    //byte[] fileBytes = await _reportService.MixinSarapReceiptsDailyReportForOfficeAsync("MixinSarapReceiptsDailyReportForOffice", "pdf", 118, fordate);

        //    //return new FileContentResult(fileBytes, mimeType)
        //    //{
        //    //    FileDownloadName = fileName
        //    //};

        //    return await _reportService.getfilelocation("MixinSarapReceiptsDailyReportForOffice", "pdf", 118, fordate);
        //}

        
        private string getReportName(string reportName, string reportType)
        {
            var outputFileName = reportName + ".pdf";

            switch (reportType.ToUpper())
            {
                default:
                case "PDF":
                    outputFileName = reportName + ".pdf";
                    break;
                case "XLSX":
                    outputFileName = reportName + ".xlsx";
                    break;
                case "DOCX":
                    outputFileName = reportName + ".docx";
                    break;
                case "HTML":
                    outputFileName = reportName + ".html";
                    break;
                case "XML":
                    outputFileName = reportName + ".xml";
                    break;
                case "IMG":
                    outputFileName = reportName + ".jpg";
                    break;
            }

            return outputFileName;
        }
    }
}
