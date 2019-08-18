using System.Collections.Generic;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.Extensions {

    public static class MapsExtensions {

        /// <summary>
        /// Gets a two-dimensional representing the points of the specified <paramref name="polygon"/>.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <returns>A two-dimensional array of <see cref="IPoint"/>.</returns>
        public static IPoint[][] GetCoordinates(this IPolygon polygon) {
            List<IPoint[]> temp = new List<IPoint[]> { polygon.Outer };
            temp.AddRange(polygon.Inner);
            return temp.ToArray();
        }

        /// <summary>
        /// Returns whether the point with the specified <paramref name="latitude"/> and <paramref name="longitude"/> is cointained by <see cref="shape"/>.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="latitude">The latitude of the point.</param>
        /// <param name="longitude">The longitude of the point.</param>
        /// <returns><c>true</c> if <paramref name="shape"/> contains the point; otherwise <c>false</c>.</returns>
        public static bool Contains(this IShape shape, double latitude, double longitude) {
            return shape.Contains(new Point(latitude, longitude));
        }

    }

}