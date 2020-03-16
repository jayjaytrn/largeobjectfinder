using LargestAreaFinder.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LargestAreaFinder
{
    class Compare
    {
        public static List<Coordinates> CheckedColors = new List<Coordinates>();
        public static List<Coordinates> UncheckedColors = new List<Coordinates>();
        public static List<Coordinates> CurrentArea = new List<Coordinates>();
        public static List<Coordinates> LargestArea = new List<Coordinates>();
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
                if (!CurrentArea.Contains(crds) && compare)
                {
                    sameColorsCoordinates.Add(crds);
                    UncheckedColors.Remove(crds);
                }
            }
            if (sameColorsCoordinates.Count == 0)
            {
                UncheckedColors.Remove(currentCoordinates);
            }
            return sameColorsCoordinates;
        }
        public List<Coordinates> GetSameColorsArea(List<Coordinates> startCoordinates, Pixel[,] array)
        {
            foreach (Coordinates crds in startCoordinates)
            {
                var nextColorsForCheck = CompareColorsAround(crds, array);
                if (nextColorsForCheck.Count == 0)
                {
                    CurrentArea = CurrentArea.Union(startCoordinates).ToList();
                }
                else
                {
                    CurrentArea = CurrentArea.Union(nextColorsForCheck).ToList();
                    GetSameColorsArea(nextColorsForCheck, array);
                }
            } 
            return CurrentArea;
        }
        public void InitUncheckedColors(Pixel[,] array)
        {
            foreach (Pixel pixel in array)
            {
                UncheckedColors.Add(pixel.Coords);
            }
        }
        public List<Coordinates> GetLargestArea(string path)
        {
            ImageWorker imgWorker = new ImageWorker();
            var bmp = imgWorker.GetImage(path);
            var pixels = imgWorker.GetPixelArray(bmp);
 
            InitUncheckedColors(pixels);

            Coordinates coordinates = new Coordinates(0, 0);
            List<Coordinates> startingCoordinates = new List<Coordinates>();
            startingCoordinates.Add(coordinates);
            while (CheckedColors.Count < (pixels.GetLength(0) * pixels.GetLength(1)))
            {
                var oneColor = GetSameColorsArea(startingCoordinates, pixels);

                try
                {
                    startingCoordinates.Clear();
                    Coordinates crdNew = UncheckedColors.First();
                    startingCoordinates.Add(crdNew);
                }
                catch(InvalidOperationException e)
                {
                }
                
                if (oneColor.Count > LargestArea.Count)
                {
                    LargestArea.Clear();
                    LargestArea = LargestArea.Union(CurrentArea).ToList();
                }
                CheckedColors = CheckedColors.Union(CurrentArea).ToList();
                CurrentArea.Clear();
            }
            return LargestArea;
        }
    }
}

