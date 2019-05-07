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

    }

}