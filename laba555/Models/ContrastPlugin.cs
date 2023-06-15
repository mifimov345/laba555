using System.Drawing;

namespace laba555.Models
{
    public class ContrastPlugin : Plugin
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ContrastPlugin(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string ProcessImage(string imagePath, string extension)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var contrastImagePath = Path.Combine(uploadsFolder, "contrast_" + Path.GetFileName(imagePath));
            AdjustContrast(imagePath, contrastImagePath);
            return contrastImagePath;
        }

        private string GetContrastImagePath(string originalImagePath)
        {
            var contrastFileName = Path.GetFileNameWithoutExtension(originalImagePath) + "_contrast" + Path.GetExtension(originalImagePath);
            var contrastImagePath = Path.Combine(Path.GetDirectoryName(originalImagePath), contrastFileName);
            return contrastImagePath;
        }

        private void AdjustContrast(string originalImagePath, string contrastImagePath)
        {
            using (var image = SixLabors.ImageSharp.Image.Load<Rgba32>(originalImagePath))
            {
                image.Mutate(x => x.Contrast(1.5f));
                image.Save(contrastImagePath);
            }
        }


        private int AdjustColorComponent(int component, float contrast)
        {
            // Применение формулы для изменения яркости и контрастности
            var adjustedComponent = (int)((component / 255.0 - 0.5) * contrast * 255 + 0.5);
            return Math.Max(0, Math.Min(255, adjustedComponent));
        }
    }

}
