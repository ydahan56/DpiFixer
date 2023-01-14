using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpiFixer.Models
{
    public class MappedImage
    {
        private readonly Bitmap raw;

        public MappedImage(string filePath)
        {
            raw = (Bitmap)Image.FromFile(filePath);
        }

        public bool HasRealDpi()
        {
            return (raw.Flags & (int)ImageFlags.HasRealDpi) > 0;
        }

        public bool HasLowDpi()
        {
            return (raw.HorizontalResolution < 96.0f) || (raw.VerticalResolution < 96.0f);
        }

        public void UpscaleDpi()
        {
            raw.SetResolution(96.0f, 96.0f);
        }

        public void SaveAs(string filePath)
        {
            raw.Save(filePath, ImageFormat.Jpeg);
        }
    }
}
