using DpiFixer.Models;
using System.Drawing;
using System.Drawing.Imaging;

string[] files = Directory.GetFiles(".\\", "*.jpg");

Console.WriteLine($"Found {files.Length} files in the current folder.");

if (files.Length == 0)
{
    return;
}

string dirName = Directory.GetCurrentDirectory();
dirName = $".\\{Path.GetFileName(dirName)}_96"; ;

if (!Directory.Exists(dirName))
{
    Directory.CreateDirectory(dirName);
    Console.WriteLine($"Created directory {dirName}.");
}

Console.WriteLine("Working.... Please wait");
int counter = 0;

foreach (string file in files)
{
    var image = new MappedImage(file);

    if (!image.HasRealDpi() || image.HasLowDpi())
    {
        image.UpscaleDpi();

        string newName = $"{dirName}\\{file}";

        image.SaveAs(newName);
        counter++;
    }
}

Console.WriteLine($"{counter} files have been fixed.");
Console.ReadKey();