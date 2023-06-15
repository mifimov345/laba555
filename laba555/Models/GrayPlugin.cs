using System.Drawing;

namespace laba555.Models
{
    public class GrayPlugin : Plugin
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GrayPlugin(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string ProcessImage(string imagePath, string extension)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var grayImagePath = Path.Combine(uploadsFolder, "gray_" + Path.GetFileName(imagePath));
            ConvertToGrayScale(imagePath, grayImagePath);
            return grayImagePath;
        }

        private string GetGrayImagePath(string originalImagePath)
        {
            var grayFileName = Path.GetFileNameWithoutExtension(originalImagePath) + "_gray" + Path.GetExtension(originalImagePath);
            var grayImagePath = Path.Combine(Path.GetDirectoryName(originalImagePath), grayFileName);
            return grayImagePath;
        }

        private void ConvertToGrayScale(string originalImagePath, string grayImagePath)
        {
            using (var image = SixLabors.ImageSharp.Image.Load<Rgba32>(originalImagePath))
            {
                image.Mutate(x => x.Grayscale());
                image.Save(grayImagePath);
            }
        }
    }

}
