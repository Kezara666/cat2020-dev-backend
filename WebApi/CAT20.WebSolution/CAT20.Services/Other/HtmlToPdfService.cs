using PuppeteerSharp;

public class HtmlToPdfService
{
    private readonly IBrowser _browser;

    public HtmlToPdfService()
    {
        _browser = Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true // Run in headless mode (without UI)
        }).Result;
    }

    public async Task<byte[]> ConvertHtmlToPdfAsync(string htmlContent)
    {
        using (var page = await _browser.NewPageAsync())
        {
            // Set the content directly from the HTML string
            await page.SetContentAsync(htmlContent, new NavigationOptions
            {
                WaitUntil = new[] { WaitUntilNavigation.Networkidle0 }
            });

            // Set PDF options
            var pdfOptions = new PdfOptions
            {
                Format = PuppeteerSharp.Media.PaperFormat.A4,
                PrintBackground = true
            };

            // Generate PDF from the webpage content
            var pdfStream = await page.PdfStreamAsync(pdfOptions);

            using (var memoryStream = new MemoryStream())
            {
                await pdfStream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
