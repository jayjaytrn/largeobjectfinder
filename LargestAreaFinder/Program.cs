using LargestAreaFinder.Model;
using System;
using System.Collections.Generic;

namespace LargestAreaFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageWorker image = new ImageWorker();
            image.GetLargestAreaAsImage("D:/test.bmp", "D:/largest.bmp");
        }
    }
}
