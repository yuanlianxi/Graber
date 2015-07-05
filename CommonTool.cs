using System;
using System.Collections.Generic;
using System.Text;
using Tesseract;
using System.Drawing;
using System.IO;
namespace Graber
{
    public class CommonTool
    {
        public static string GetTextFromImage(string imagePath, string language = "eng")
        {
            TesseractEngine engine = new TesseractEngine("tessdata", language, EngineMode.TesseractOnly);

            Image image = Bitmap.FromFile(imagePath);
            Page page = engine.Process(image as Bitmap);
            image.Dispose();
            return page.GetText();


        }
        public static string GetTextFromImage(Stream imageStream, string language = "eng")
        {
            TesseractEngine engine = new TesseractEngine("tessdata", language, EngineMode.TesseractOnly);

            Image image = Bitmap.FromStream(imageStream);
            Page page = engine.Process(image as Bitmap);
            image.Dispose();
            return page.GetText();


        }
    }
}
