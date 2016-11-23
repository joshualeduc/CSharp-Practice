using System;

namespace Polygons.Library
{
    public class Triangle: AbstractRegularPolygon
    {
        public Triangle(int length) :
            base(3, length) { }

        public override double GetArea() //Will throw a compile error if commented out
        {
            return SideLength * SideLength * Math.Sqrt(3) / 4;
        }
    }
}