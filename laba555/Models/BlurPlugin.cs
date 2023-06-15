using System.Drawing;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
namespace laba555.Models

{
    public class BlurPlugin : Plugin
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlurPlugin(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string ProcessImage(string imagePath, string extension)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var blurImagePath = Path.Combine(uploadsFolder, "blur_" + Path.GetFileName(imagePath));
            ApplyBlurFilter(imagePath, blurImagePath);
            return blurImagePath;
        }

        private void ApplyBlurFilter(string originalImagePath, string blurImagePath)
        {
            using (var image = SixLabors.ImageSharp.Image.Load(originalImagePath))
            {
                image.Mutate(x => x.GaussianBlur());
                image.Save(blurImagePath);
            }
        }
    }
}
