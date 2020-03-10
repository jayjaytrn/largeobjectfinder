using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
        public Bitmap CloneBitmap(string path)
        {
            var bitmap = GetImage(path);
            Bitmap cloneBitmap = (Bitmap)bitmap.Clone();
            return cloneBitmap;
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
        public void X(string path)
        {
            var cloneBitmap = CloneBitmap(path);

            var resultImage = new Bitmap(cloneBitmap.Width, cloneBitmap.Height);

            Compare compare = new Compare();
            var largestArea = compare.GetLargestArea();

            foreach (Coordinates crds in largestArea)
            {
                resultImage.SetPixel(crds.x, crds.y, cloneBitmap.GetPixel(crds.x, crds.y));
            }
            resultImage.Save("D:/testtest.png", ImageFormat.Png);
        }
    }
}
