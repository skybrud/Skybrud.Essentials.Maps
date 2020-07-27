using System.Collections.Generic;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.Extensions {

    /// <summary>
    /// Static class with various extension methods for use with this package.
    /// </summary>
    public static class MapsExtensions {

        /// <summary>
        /// Returns a two-dimensional array representing the points of the specified <paramref name="polygon"/>.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <returns>A two-dimensional array of <see cref="IPoint"/>.</returns>
        public static IPoint[][] GetCoordinates(this IPolygon polygon) {
            List<IPoint[]> temp = new List<IPoint[]> { polygon.Outer };
            temp.AddRange(polygon.Inner);
            return temp.ToArray();
        }

        /// <summary>
        /// Returns whether the point with the specified <paramref name="latitude"/> and <paramref name="longitude"/> is cointained by <see cref="IShape"/>.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="latitude">The latitude of the point.</param>
        /// <param name="longitude">The longitude of the point.</param>
        /// <returns><c>true</c> if <paramref name="shape"/> contains the point; otherwise <c>false</c>.</returns>
        public static bool Contains(this IShape shape, double latitude, double longitude) {
            return shape.Contains(new Point(latitude, longitude));
        }

        /// <summary>
        /// Returns an array with the X and Y coordinates of the specified <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The point to bee converted.</param>
        /// <returns>An array of <see cref="double"/> representing the coordinates of <paramref name="point"/>.</returns>
        public static double[] ToXyArray(this IPoint point) {
            return new[] {point.Longitude, point.Latitude};
        }

    }

}