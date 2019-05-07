using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.Geometry.Lines {

    public interface ILineBase : IGeometry {

        /// <summary>
        /// Gets the total length of the line.
        /// </summary>
        /// <returns></returns>
        double GetLength();

        /// <summary>
        /// Gets a point representing the center of the line.
        /// </summary>
        /// <returns></returns>
        IPoint GetCenter();

        /// <summary>
        /// Gets the bounding box surrounding the line.
        /// </summary>
        /// <returns>An instance of <see cref="IRectangle"/>.</returns>
        IRectangle GetBoundingBox();

    }

}