namespace Skybrud.Essentials.Maps.Geometry.Shapes {

    /// <summary>
    /// Interface representing a rectangle.
    /// </summary>
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/Rectangle</cref>
    /// </see>
    public interface IRectangle : IShape {

        /// <summary>
        /// Gets the south west point.
        /// </summary>
        IPoint SouthWest { get; }

        /// <summary>
        /// Gets or the north east point.
        /// </summary>
        IPoint NorthEast { get; }

    }

}