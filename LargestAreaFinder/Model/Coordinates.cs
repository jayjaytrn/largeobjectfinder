using System;
using System.Collections.Generic;
using System.Text;

namespace LargestAreaFinder.Model
{
    struct Coordinates
    {
        public int x { get; set; }
        public int y { get; set; }
        public Coordinates(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
        }
        public bool IsItBorderline(Pixel[,] pixels, Coordinates currentCoordinates, Coordinates nextCoordinates)
        {
            if (nextCoordinates.x == currentCoordinates.x - 1 && currentCoordinates.x - 1 < 0)
            {
                return true;
            }
            else if (nextCoordinates.x == currentCoordinates.x + 1 && currentCoordinates.x + 1 >= pixels.GetLength(0))
            {
                return true;
            }
            else if (nextCoordinates.y == currentCoordinates.y - 1 && currentCoordinates.y - 1 < 0)
            {
                return true;
            }
            else if (nextCoordinates.y == currentCoordinates.y + 1 && currentCoordinates.y + 1 >= pixels.GetLength(1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Coordinates> CoordinatesAround(Pixel[,] pixels, Coordinates currentCoordinates)
        {
            var expectdotsaround = new List<Coordinates>();
            expectdotsaround.Add(new Coordinates(currentCoordinates.x - 1, currentCoordinates.y)); //left
            expectdotsaround.Add(new Coordinates(currentCoordinates.x + 1, currentCoordinates.y)); //right
            expectdotsaround.Add(new Coordinates(currentCoordinates.x, currentCoordinates.y - 1)); //up
            expectdotsaround.Add(new Coordinates(currentCoordinates.x, currentCoordinates.y + 1)); //down

            var dotsaround = new List<Coordinates>();

            foreach (Coordinates crds in expectdotsaround)
            {
                if (!IsItBorderline(pixels, currentCoordinates, crds))
                {
                    dotsaround.Add(crds);
                }
            }
            return dotsaround;
        }

        public override bool Equals(object obj)
        {
            return obj is Coordinates coordinates &&
                   x == coordinates.x &&
                   y == coordinates.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
    }
}
