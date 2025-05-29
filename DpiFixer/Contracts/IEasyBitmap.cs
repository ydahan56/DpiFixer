using DpiFixer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpiFixer.Contracts
{
    public interface IEasyBitmap
    {
        void UpscaleCallback(object? sender, EventArgs e);
    }
}
