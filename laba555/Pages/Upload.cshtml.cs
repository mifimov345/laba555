using laba555.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace laba555.Pages
{
    public class UploadModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PluginsConfiguration _pluginsConfiguration;

        public List<string> GrayImagePaths { get; set; }
        public List<string> ContrastImagePaths { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public string ImagePath { get; set; }
        public string GrayImagePath { get; set; }
        public string ContrastImagePath { get; set; }

        public string BlurImagePath { get; set; }

        public UploadModel(IWebHostEnvironment webHostEnvironment, PluginsConfiguration pluginsConfiguration)
        {
            _webHostEnvironment = webHostEnvironment;
            _pluginsConfiguration = pluginsConfiguration;
        }

        public void OnPost()
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".png" };
                var fileExtension = Path.GetExtension(ImageFile.FileName);

                // Check if the file extension is allowed
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ImageFile", "Only .jpg and .png files are allowed.");
                    return;
                }

                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Path.GetRandomFileName() + fileExtension;
                var imagePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                }

                ImagePath = "/uploads/" + uniqueFileName;

                var imageProcessor = new ImageProcessor();

                // Apply gray filter
                GrayImagePath = imageProcessor.ConvertToGray(imagePath, fileExtension);
                GrayImagePath = "/uploads/" + Path.GetFileName(GrayImagePath);

                // Apply contrast filter
                ContrastImagePath = imageProcessor.IncreaseContrast(imagePath, fileExtension);
                ContrastImagePath = "/uploads/" + Path.GetFileName(ContrastImagePath);

                // Apply blur filter
                var blurImagePath = imageProcessor.ApplyBlur(imagePath, fileExtension);
                BlurImagePath = "/uploads/" + Path.GetFileName(blurImagePath);
            }
        }

    }
}
