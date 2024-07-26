using System.Drawing;
using System.Drawing.Imaging;

namespace DpiFixer.Models
{
    public class EasyBitmap
    {
        private readonly Bitmap _bitmap;

        public EasyBitmap(string filePath)
        {
            _bitmap = (Bitmap)Image.FromFile(filePath);
        }

        public bool HasRealDpi()
        {
            return (_bitmap.Flags & (int)ImageFlags.HasRealDpi) > 0;
        }

        public bool HasLowDpi()
        {
            return (_bitmap.HorizontalResolution < 96.0f) || (_bitmap.VerticalResolution < 96.0f);
        }

        public void UpscaleDpi()
        {
            _bitmap.SetResolution(96.0f, 96.0f);
        }

        public void SaveAs(string directory, string fileName)
        {
            _bitmap.Save(Path.Combine(directory, fileName), ImageFormat.Jpeg);
        }
    }
}
