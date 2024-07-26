using DpiFixer.Models;
using System.Reflection;

string baseDirectory = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
string[] files = Directory.GetFiles(baseDirectory, "*.jpg");

Console.WriteLine($"Found {files.Length} files in the current folder.");

if (files.Length == 0)
{
    return;
}

string targetDirectory = $"{baseDirectory}_96";

if (!Directory.Exists(targetDirectory))
{
    Directory.CreateDirectory(targetDirectory);
    Console.WriteLine($"Created directory {targetDirectory}.");
}

Console.WriteLine("Working.... Please wait");
int counter = 0;

foreach (string fileName in files)
{
    var easyBmp = new EasyBitmap(fileName);

    if (!easyBmp.HasRealDpi() || easyBmp.HasLowDpi())
    {
        easyBmp.UpscaleDpi();
        easyBmp.SaveAs(targetDirectory, fileName);
        
        counter++;
    }
}

Console.WriteLine($"{counter} files have been fixed.");
Console.ReadKey();