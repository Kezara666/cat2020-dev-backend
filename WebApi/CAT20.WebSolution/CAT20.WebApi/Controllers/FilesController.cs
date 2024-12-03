using CAT20.WebApi.Configuration;
using CAT20.WebApi.Controllers.Control;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.IO;

namespace CAT20.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _uploadsFolder;

        public FilesController(IWebHostEnvironment environment, IOptions<AppSettings> appSettings)
        {
            _environment = environment;
            _uploadsFolder = appSettings.Value.UploadsFolder;
        }


        [HttpGet("retrieve/{fileName}")]
        public IActionResult RetrieveFile(string fileName)
        {
            try
            {
                string filePath = Path.Combine(_uploadsFolder, fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                var fileBytes = System.IO.File.ReadAllBytes(filePath);

                string contentType;
                if (IsImageFile(fileName))
                {
                    contentType = GetImageContentType(fileName);
                }
                else if (IsPdfFile(fileName))
                {
                    contentType = "application/pdf";
                }
                else
                {
                    contentType = "application/octet-stream";
                }

                return File(fileBytes, contentType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private bool IsImageFile(string fileName)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            string extension = Path.GetExtension(fileName).ToLower();
            return imageExtensions.Contains(extension);
        }

        private string GetImageContentType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".bmp":
                    return "image/bmp";
                default:
                    return "application/octet-stream"; // Default to binary for unknown image types
            }
        }

        private bool IsPdfFile(string fileName)
        {
            string[] pdfExtensions = { ".pdf" };
            string extension = Path.GetExtension(fileName).ToLower();
            return pdfExtensions.Contains(extension);
        }

        [HttpGet("cust_photo/{fileName}")]
        public IActionResult RetrieveCustomerPhoto(string fileName)
        {
            try
            {
                string filePath = Path.Combine(_uploadsFolder+"/Customers/ProfilePhotos", fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                var fileBytes = System.IO.File.ReadAllBytes(filePath);

                string contentType;
                if (IsImageFile(fileName))
                {
                    contentType = GetImageContentType(fileName);
                }
                else if (IsPdfFile(fileName))
                {
                    contentType = "application/pdf";
                }
                else
                {
                    contentType = "application/octet-stream";
                }

                return File(fileBytes, contentType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("cust_docs/{fileName}")]
        public IActionResult RetrieveCustomerDocument(string fileName)
        {
            try
            {
                string filePath = Path.Combine(_uploadsFolder + "/Customers/Documents", fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                byte[] fileBytes;

                if (IsImageFile(fileName))
                {
                    using (Image image = Image.Load(filePath))  
                    {
                        using (var ms = new MemoryStream())
                        {
                            // Reduce image quality (e.g., 70% quality)
                            var jpegEncoder = new JpegEncoder { Quality = 70 };
                            image.SaveAsJpeg(ms, jpegEncoder);  // Save as JPEG with reduced quality
                            fileBytes = ms.ToArray();  // Get compressed image bytes
                        }
                    }

                    string contentType = GetImageContentType(fileName);
                    return File(fileBytes, contentType);
                }
                // For PDFs or other files
                else if (IsPdfFile(fileName))
                {
                    fileBytes = System.IO.File.ReadAllBytes(filePath);

                    string contentType = "application/pdf";
                    return File(fileBytes, contentType);
                }
                else
                {
                    fileBytes = System.IO.File.ReadAllBytes(filePath);
                    string contentType = "application/octet-stream";
                    return File(fileBytes, contentType);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[HttpGet("cust_docs/{fileName}")]
        //public IActionResult RetrieveCustomerDocument(string fileName)
        //{
        //    try
        //    {
        //        string filePath = Path.Combine(_uploadsFolder + "/Customers/Documents", fileName);

        //        if (!System.IO.File.Exists(filePath))
        //        {
        //            return NotFound("File not found.");
        //        }

        //        var fileBytes = System.IO.File.ReadAllBytes(filePath);

        //        string contentType;
        //        if (IsImageFile(fileName))
        //        {
        //            contentType = GetImageContentType(fileName);
        //        }
        //        else if (IsPdfFile(fileName))
        //        {
        //            contentType = "application/pdf";
        //        }
        //        else
        //        {
        //            contentType = "application/octet-stream";
        //        }

        //        return File(fileBytes, contentType);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
    }
}