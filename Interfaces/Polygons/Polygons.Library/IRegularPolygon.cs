
namespace Polygons.Library
{
    //Interfaces throw compile errors
    //Interfaces public declarations only
    //Multiple interfaces can be used
    //Limited to properties, methods, events, and indexers
    public interface IRegularPolygon
    {
        int NumberOfSides { get; set; }
        int SideLength { get; set; }

        double GetPerimeter();
        double GetArea();
    }
}
