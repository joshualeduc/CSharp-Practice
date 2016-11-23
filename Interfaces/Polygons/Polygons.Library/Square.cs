
namespace Polygons.Library
{
    public class Square : ConcreteRegularPolygon
    {
        public Square(int length) :
            base(4, length) { }

        public override double GetArea() //Throws a runtime error if commented out
        {
            return SideLength * SideLength;
        }
    }
}
