using CAT20.Core.Models.Vote;
using CAT20.Services.Control;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;
using CAT20.Core.Models.WaterBilling;
using System.Reflection.PortableExecutable;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace CAT20.Services.Control
{
    public class DocumentService : IDocumentService
    {
        private readonly IConverter _converter;

        public DocumentService(
            IConverter converter
            )
        {
            _converter = converter;
        }

        public sealed class Dummy
        {

            public static readonly IConverter converter = new SynchronizedConverter(new PdfTools());

            private Dummy() { }

        }

        //waterbill PDF generation
        public byte[] GenerateWaterBillPdfFromWaterConnectionBalancesList(IEnumerable<WaterConnectionBalance> WaterConnectionBalances)
        {
            var pdfBytesList = new List<byte[]>();
            //var converter = new BasicConverter(new PdfTools());
            
            foreach (var wbl in WaterConnectionBalances)
            {
                var htmlContent = $@"
                   <!DOCTYPE html>
                    <html lang=""""en"""">
                    <head>
                    </head>
                    <body>
                    <h6 style="" position: absolute; top: 1.5in; left: 1.5in;""> {wbl.BarCode} </h6>
                    <h6 style="" position: absolute; top: 1.5in; left: 5.5in;""> {wbl.Year}-{wbl.Month}</h6>
                    <h6 style="" position: absolute; top: 4in; left: 3.3in;""> {wbl.PrintBillingDetails} </h6>
                    <h6 style="" position: absolute; top: 5.5in; left: 2.5in;""> {wbl.PreviousMeterReading} </h6>
                    <h6 style="" position: absolute; top: 5.5in; left: 6.3in;""> {wbl.PrintLastBalance} </h6>
                    <h6 style="" position: absolute; top: 7.3in; left: 2.5in;""> {wbl.BillProcessDate} </h6>
                    </body>
                    </html>
                ";



                var currentPdfBytes = GenerateWaterBillPdf(htmlContent, PaperKind.Letter, DinkToPdf.Orientation.Landscape);
                pdfBytesList.Add(currentPdfBytes);
            }
            return MergePdfFiles(pdfBytesList);
        }


        private static byte[] GenerateWaterBillPdf(string htmlContent, PaperKind paperSize, DinkToPdf.Orientation orientation)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = orientation,
                PaperSize = paperSize,
                //Margins = new MarginSettings { Top = 18, Bottom = 18 },
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8" },
                //HeaderSettings = { FontSize = 10, Right = "Page [page] of [toPage]", Line = true },
                //FooterSettings = { FontSize = 8, Center = "CAT20 - Wayamba Developement Authority", Line = true },
            };

            var htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };

            byte[] pdfBytes = Dummy.converter.Convert(htmlToPdfDocument);
            return pdfBytes;
            //return converter.Convert(htmlToPdfDocument);
        }

        //end of PDF Generation







        public byte[] GeneratePdfFromString()
        {
            int pageCount = 100;
            var pdfBytesList = new List<byte[]>();

            var converter = new BasicConverter(new PdfTools());

            for (int i = 1; i <= pageCount; i++)
            {
                var htmlContent = $@"
                    <!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                        <style>
                        p {{
                            width: 80%;
                        }}
                        </style>
                    </head>
                    <body>
                        <h1> ආයුබෝවන් | வரவேற்பு | Welcome</h1>
                        <p>This is page {i} content.</p>
                    </body>
                    </html>
                ";

                var currentPdfBytes = GeneratePdf(htmlContent, PaperKind.Letter, DinkToPdf.Orientation.Landscape, converter);
                pdfBytesList.Add(currentPdfBytes);
            }

            return MergePdfFiles(pdfBytesList);
        }

        private static byte[] GeneratePdf(string htmlContent, PaperKind paperSize, DinkToPdf.Orientation orientation, BasicConverter converter)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = orientation,
                PaperSize = paperSize,
                //Margins = new MarginSettings { Top = 18, Bottom = 18 },
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8" },
                //HeaderSettings = { FontSize = 10, Right = "Page [page] of [toPage]", Line = true },
                //FooterSettings = { FontSize = 8, Center = "CAT20 - Wayamba Developement Authority", Line = true },
            };

            var htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };

            return converter.Convert(htmlToPdfDocument);
        }

        public static byte[] MergePdfFiles(List<byte[]> pdfBytesList)
        {
            using (var ms = new MemoryStream())
            {
                using (var mergedPdfDocument = new PdfDocument())
                {
                    foreach (var pdfBytes in pdfBytesList)
                    {
                        using (var pdfStream = new MemoryStream(pdfBytes))
                        {
                            var pdf = PdfReader.Open(pdfStream, PdfDocumentOpenMode.Import);
                            foreach (PdfPage page in pdf.Pages)
                            {
                                mergedPdfDocument.AddPage(page);
                            }
                        }
                    }
                    mergedPdfDocument.Save(ms);
                }
                return ms.ToArray();
            }
        }

        //private static byte[] MergePdfFiles(List<byte[]> pdfBytesList)
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        using (var mergedPdfDocument = new PdfDocument(new PdfWriter(ms)))
        //        {
        //            foreach (var pdfBytes in pdfBytesList)
        //            {
        //                using (var pdfReader = new PdfReader(new MemoryStream(pdfBytes)))
        //                {
        //                    var pdf = new PdfDocument(pdfReader);
        //                    pdf.CopyPagesTo(1, pdf.GetNumberOfPages(), mergedPdfDocument);
        //                }
        //            }
        //        }
        //        return ms.ToArray();
        //    }
        //}
    }
}

