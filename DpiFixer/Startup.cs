using DpiFixer.Contracts;
using DpiFixer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DpiFixer
{
    public class Startup
    {
        private event EventHandler? UpscaleEvent;

        private string root;
        private string dest;

        public Startup()
        {
            this.root = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
        }

        public void Initialize()
        {
            this.dest = Path.Combine(root!, DateTime.Now.ToString("ddMMyyHHmm"));
            if (!Directory.Exists(dest))
            {
                Directory.CreateDirectory(dest);
                Console.WriteLine($"Created directory {dest}.");
            }

            var files = Directory.GetFiles(root!, "*.jpg").Select(HandleSelect).ToList();

            if (files.Count == 0)
            {
                Console.WriteLine("No files exists in the current directory.");
                return;
            }

            Console.WriteLine($"Processing {files.Count} files, Please wait....");
            this.UpscaleEvent?.Invoke(this, EventArgs.Empty);
        }

        private EasyBitmap HandleSelect(string fileName)
        {
            var easy = new EasyBitmap(this.dest, fileName);
            this.UpscaleEvent += easy.UpscaleCallback;

            return easy;
        }
    }
}
