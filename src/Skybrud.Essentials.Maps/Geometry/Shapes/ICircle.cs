namespace Skybrud.Essentials.Maps.Geometry.Shapes {
    
    /// <summary>
    /// Interface describing a circle on a spheroid.
    /// </summary>
    public interface ICircle : IShape {

        /// <summary>
        /// Gets the center of the circle.
        /// </summary>
        IPoint Center { get; }

        /// <summary>
        /// Gets the radius of the circle.
        /// </summary>
        double Radius { get; }

    }

}