using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Compression;

namespace DpiFixer.Models
{
    public class EasyBitmap
    {
        private readonly string? _dest;
        private readonly string? _fileName;

        private readonly Bitmap? _bitmap;

        public EasyBitmap(string? dest, string? filePath)
        {
            _dest = dest ?? throw new ArgumentNullException(nameof(dest));
            _fileName = Path.GetFileName(filePath) ?? throw new ArgumentNullException(nameof(filePath));

            _bitmap = (Bitmap)Image.FromFile(filePath!);
        }

        private bool HasRealDpi()
        {
            return (_bitmap.Flags & (int)ImageFlags.HasRealDpi) > 0;
        }

        private bool HasLowDpi()
        {
            return (_bitmap.HorizontalResolution < 96.0f) || (_bitmap.VerticalResolution < 96.0f);
        }

        private void UpscaleDpi()
        {
            _bitmap.SetResolution(96.0f, 96.0f);
        }

        private bool NeedsUpscale()
        {
            return (!this.HasRealDpi() || this.HasLowDpi());
        }

        private void Save()
        {
            _bitmap.Save(Path.Combine(this._dest, this._fileName), ImageFormat.Jpeg);
        }

        public void UpscaleCallback(object? sender, EventArgs e)
        {
            if (this.NeedsUpscale())
            {
                this.UpscaleDpi();
                this.Save();
            }
        }
    }
}
