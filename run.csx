using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public static void Run(Stream inputFile, Stream outputFile, TraceWriter log)
{
    log.Info($"OneDrive trigger function processed new file");    
    AddWatermark("Mark C. Fernando", inputFile, outputFile, log);
    log.Info("The watermark has been added.");
}

private static void AddWatermark(string watermarkContent, Stream inputStream, Stream outputStream, TraceWriter log)
{
    using (Image inputImage = Image.FromStream(inputStream, true))
    {
        using (Graphics graphic = Graphics.FromImage(inputImage))
        {
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.DrawString(watermarkContent, new Font("Tahoma", 100, FontStyle.Bold), Brushes.Red, 200, 200);
            graphic.Flush();

            log.Info("Write to the output stream");
            inputImage.Save(outputStream, ImageFormat.Jpeg);
        }
    }
}