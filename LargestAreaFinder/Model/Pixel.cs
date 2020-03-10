using System;
using System.Collections.Generic;
using System.Text;

namespace LargestAreaFinder.Model
{
    class Pixel
    {
        public Coordinates Coords { get; set; }
        public string ColorName { get; set; }
        public Pixel(Coordinates coordinates, string _ColorName)
        {
            this.Coords = new Coordinates(coordinates.x, coordinates.y);
            this.ColorName = _ColorName;
        }
    }
}
