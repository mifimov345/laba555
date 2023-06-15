using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace laba555.Models
{
    public class ImageProcessor
    {
        public string ConvertToGray(string imagePath, string extension)
        {
            var grayImagePath = GetNewImagePath(imagePath, "_gray", extension);

            using (var image = Image.Load(imagePath))
            {
                image.Mutate(x => x.Grayscale());
                image.Save(grayImagePath);
            }

            return Path.GetFileName(grayImagePath);
        }

        public string IncreaseContrast(string imagePath, string extension)
        {
            var contrastImagePath = GetNewImagePath(imagePath, "_contrast", extension);

            using (var image = Image.Load(imagePath))
            {
                image.Mutate(x => x.Contrast(1.5f));
                image.Save(contrastImagePath);
            }

            return Path.GetFileName(contrastImagePath);
        }

        public string ApplyBlur(string imagePath, string extension)
        {
            var blurImagePath = GetNewImagePath(imagePath, "_blur", extension);

            using (var image = Image.Load(imagePath))
            {
                image.Mutate(x => x.GaussianBlur());
                image.Save(blurImagePath);
            }

            return Path.GetFileName(blurImagePath);
        }

        private string GetNewImagePath(string imagePath, string suffix, string extension)
        {
            var directory = Path.GetDirectoryName(imagePath);
            var fileName = Path.GetFileNameWithoutExtension(imagePath);
            var newFileName = $"{fileName}{suffix}{extension}";
            return Path.Combine(directory, newFileName);
        }
    }
}
