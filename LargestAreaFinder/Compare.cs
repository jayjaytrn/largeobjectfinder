using LargestAreaFinder.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LargestAreaFinder
{
    class Compare
    {
        public List<Coordinates> LargestArea = new List<Coordinates>();
        public bool CompareColors(string initialColor, string comparedColor)
        {
            return (initialColor.Equals(comparedColor));
        }
        public List<Coordinates> CompareColorsAround(Coordinates currentCoordinates, Pixel[,] array)
        {
            var sameColorsCoordinates = new List<Coordinates>();

            Coordinates coordinates = new Coordinates();
            List<Coordinates> coordinatesAround = coordinates.CoordinatesAround(array, currentCoordinates);

            foreach (Coordinates crds in coordinatesAround)
            {
                var compare = CompareColors(array[currentCoordinates.x, currentCoordinates.y].ColorName, array[crds.x, crds.y].ColorName);
                if (!LargestArea.Contains(crds) && compare)
                {
                    sameColorsCoordinates.Add(crds);
                }
            }
            return sameColorsCoordinates;
        }
        public List<Coordinates> FindAllSameColors(List<Coordinates> startCoordinates, Pixel[,] array)
        {
            foreach (Coordinates crds in startCoordinates)
            {
                var nextColorsForCheck = CompareColorsAround(crds, array);
                LargestArea = LargestArea.Union(nextColorsForCheck).ToList();
                var allCoordinates = FindAllSameColors(nextColorsForCheck, array);
            }
            return LargestArea;
        }
        public void Runner()
        {
            ImageWorker imgWorker = new ImageWorker();
            var bmp = imgWorker.GetImage("E:/test.bmp");
            var pixels = imgWorker.GetPixelArray(bmp);

            Compare compare = new Compare();
            Coordinates coordinates = new Coordinates(0, 0);
            List<Coordinates> startingCoordinates = new List<Coordinates>();
            startingCoordinates.Add(coordinates);
            var x = compare.FindAllSameColors(startingCoordinates, pixels);
            Console.WriteLine(x.Count);
            Console.ReadLine();
        }
    }
}

