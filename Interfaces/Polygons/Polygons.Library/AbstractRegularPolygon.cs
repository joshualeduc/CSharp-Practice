
namespace Polygons.Library
{
    //Abstract parts can throw compile errors
    //Abstract classes can offer some implementation
    //Abstract classes can have different accessibility properties
    //Only one abstract class can be inherited
    public abstract class AbstractRegularPolygon
    {
        public int NumberOfSides { get; set; }
        public int SideLength { get; set; }

        public AbstractRegularPolygon(int sides, int length)
        {
            NumberOfSides = sides;
            SideLength = length;
        }

        public double GetPerimeter()
        {
            return NumberOfSides * SideLength;
        }

        public abstract double GetArea();
    }
}
