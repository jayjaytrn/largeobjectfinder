using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using LargestAreaFinder.Model;

namespace LargestAreaFinder
{
    class ImageWorker
    {
        public Bitmap GetImage(string path)
        {
            var image = new Bitmap(path);
            return image;
        }
        public Pixel[,] GetPixelArray(Bitmap bitmap)
        {
            var height = bitmap.Height;
            var width = bitmap.Width;
            Pixel[,] array = new Pixel[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Coordinates crd = new Coordinates(x, y);
                    var colorName = bitmap.GetPixel(x, y).Name;
                    Pixel pixel = new Pixel(crd, colorName);
                    array.SetValue(pixel, x, y);
                }
            }
            return array;
        }
        public void GetLargestAreaAsImage(string initialPath, string pathToSaveLargestArea)
        {
            var bitmap = new Bitmap(initialPath);

            var resultImage = new Bitmap(bitmap.Width, bitmap.Height);

            Compare compare = new Compare();
            var largestArea = compare.GetLargestArea(initialPath);

            //foreach (Coordinates crds in largestArea)
            //{
            //    resultImage.SetPixel(crds.x, crds.y, bitmap.GetPixel(crds.x, crds.y));
            //}
            largestArea.AsParallel()
            .ForAll(crds => resultImage.SetPixel(crds.x, crds.y, bitmap.GetPixel(crds.x, crds.y)));

            //resultImage.Save(pathToSaveLargestArea, ImageFormat.Png);
        }
    }
}
