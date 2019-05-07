namespace Skybrud.Essentials.Maps.Geometry.Shapes {
    
    /// <summary>
    /// Interface represeting a polygon.
    /// </summary>
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/Polygon</cref>
    /// </see>
    public interface IPolygon : IShape {

        /// <summary>
        /// Gets the array of the outer coordinates of the polygon.
        /// </summary>
        IPoint[] Outer { get; }

        /// <summary>
        /// Gets the arrays of the inner coordinates of any inner polygon.
        /// </summary>
        IPoint[][] Inner { get; }

    }

}