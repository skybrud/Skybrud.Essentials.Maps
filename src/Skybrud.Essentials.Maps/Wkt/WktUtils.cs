using System;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.Wkt {

    public static class WktUtils {

        /// <summary>
        /// Converts the specified <paramref name="point"/> to a correspoinding Well Known Text point.
        /// </summary>
        /// <param name="point">The point to be converted.</param>
        /// <returns>An instance of <see cref="WktPoint"/>.</returns>
        public static WktPoint ToWkt(IPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            return new WktPoint(point);
        }

        /// <summary>
        /// Converts the specified Well Known Text <paramref name="point"/> to the corresponding GPS point.
        /// </summary>
        /// <param name="point">The Well Known Text point.</param>
        /// <returns>An instance of <see cref="IPoint"/> </returns>
        public static IPoint ToPoint(this WktPoint point) {
            return new Point(point.Y, point.X);
        }

        public static WktPoint[][] ToWktPoints(IPolygon polygon) {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            return WktUtils.ToWkt(MapsUtils.GetCoordinates(polygon));
        }

        public static WktPolygon ToWkt(IPolygon polygon) {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            return new WktPolygon(polygon);
        }

        public static WktPoint[][] ToWkt(IPoint[][] coordinates) {

            if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));

            WktPoint[][] temp = new WktPoint[coordinates.Length][];

            for (int i = 0; i < coordinates.Length; i++) {
                temp[i] = new WktPoint[coordinates[i].Length];
                for (int j = 0; j < temp[i].Length; j++) {
                    temp[i][j] = new WktPoint(coordinates[i][j]);
                }
            }

            return temp;

        }

    }

}