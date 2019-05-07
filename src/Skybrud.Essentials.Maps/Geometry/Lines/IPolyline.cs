namespace Skybrud.Essentials.Maps.Geometry.Lines {
    
    /// <summary>
    /// Interface representing a polyline.
    /// </summary>
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/Polygonal_chain</cref>
    /// </see>
    public interface IPolyline : ILineBase {

        /// <summary>
        /// Gets the array of points making up the polyline.
        /// </summary>
        IPoint[] Points { get; }

    }

}