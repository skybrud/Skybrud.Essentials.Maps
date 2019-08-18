using System;
using System.Collections.Generic;
using System.Linq;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Shapes;

using static Skybrud.Essentials.Maps.MapsUtils;

namespace Skybrud.Essentials.Maps {

    /// <summary>
    /// Static class with various utility/helper methods for working with polygons.
    /// </summary>
    public class PolygonUtils {

        /// <summary>
        /// Returns a two-dimensional array representing the points of the specified <paramref name="polygon"/>.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <returns>A two-dimensional array of <see cref="IPoint"/>.</returns>
        public static IPoint[][] GetCoordinates(IPolygon polygon) {
            List<IPoint[]> temp = new List<IPoint[]> { polygon.Outer };
            temp.AddRange(polygon.Inner);
            return temp.ToArray();
        }

        /// <summary>
        /// Returns the area of the polygon in square metres. 
        /// </summary>
        /// <param name="points">The points making up the polygon.</param>
        /// <returns>Returns a <see cref="double"/> representing the area in square metres.</returns>
        /// <see>
        ///     <cref>https://developers.google.com/maps/documentation/javascript/reference/3/geometry#spherical.computeArea</cref>
        /// </see>
        public static double GetArea(IPoint[] points) {
            return GetArea(points, EarthConstants.EquatorialRadius);
        }

        /// <summary>
        /// Returns the area of the polygon in square metres. 
        /// </summary>
        /// <param name="points">The points making up the polygon.</param>
        /// <param name="radius">The radius of the spheroid.</param>
        /// <returns>Returns a <see cref="double"/> representing the area in square metres.</returns>
        public static double GetArea(IPoint[] points, double radius) {

            // Since the polygon is projected on a sphere (eg. Earth), the steps involved for
            // calculating the area is a bit more advanced than when calculating the area of a
            // regular polygon
            double metresPerDegree = 2.0 * Math.PI * radius / 360.0;

            double area = 0;
            for (int i = 0; i < points.Length; ++i) {
                int j = (i + 1) % points.Length;
                double xi = points[i].Longitude * metresPerDegree * Math.Cos(DegreesToRadians(points[i].Latitude));
                double yi = points[i].Latitude * metresPerDegree;
                double xj = points[j].Longitude * metresPerDegree * Math.Cos(DegreesToRadians(points[j].Latitude));
                double yj = points[j].Latitude * metresPerDegree;
                area += xi * yj - xj * yi;
            }

            return Math.Abs(area / 2.0);

        }

        /// <summary>
        /// Returns the circumference of the polygon represented by the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points making up the polygon.</param>
        /// <returns>The circumference in metres.</returns>
        public static double GetCircumference(IPoint[] points) {
            
            double sum = 0;

            // Iterate through each point in the path
            for (int i = 0; i < points.Length; i++) {

                // While "i" is the index of the first point and "j" is the second point
                int j = i == 0 ? points.Length - 1 : i - 1;

                // Calculate the distance between the two points
                sum += DistanceUtils.GetDistance(points[i], points[j]);

            }

            return sum;

        }

        /// <summary>
        /// Returns whether the polygon defined by the specified <paramref name="coordinates"/> contains <paramref name="point"/>.
        /// </summary>
        /// <param name="coordinates">The coordinates of the polygon.</param>
        /// <param name="point">The point.</param>
        /// <returns><c>true</c> if the polygon contains <paramref name="point"/>, otherwise <c>false</c>.</returns>
        public static bool Contains(IPoint[] coordinates, IPoint point) {

            int i, j;
            bool c = false;

            for (i = 0, j = coordinates.Length - 1; i < coordinates.Length; j = i++) {
                if ((((coordinates[i].Latitude <= point.Latitude) && (point.Latitude < coordinates[j].Latitude))
                     || ((coordinates[j].Latitude <= point.Latitude) && (point.Latitude < coordinates[i].Latitude)))
                    && (point.Longitude < (coordinates[j].Longitude - coordinates[i].Longitude) * (point.Latitude - coordinates[i].Latitude)
                        / (coordinates[j].Latitude - coordinates[i].Latitude) + coordinates[i].Longitude)) {
                        c = !c;
                    }
            }

            return c;

        }

        /// <summary>
        /// Returns an instance of <see cref="IRectangle"/> representing the bounding box of the polygon with the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns>An instance of <see cref="IRectangle"/>.</returns>
        public static IRectangle GetBoundingBox(IEnumerable<IPoint> points) {

            if (points == null) throw new ArgumentNullException(nameof(points));

            IPoint[] array = points.ToArray();

            double lat1 = array[0].Latitude;
            double lat2 = array[0].Latitude;

            double lng1 = array[0].Longitude;
            double lng2 = array[0].Longitude;

            foreach (IPoint point in array) {

                lat1 = Math.Min(lat1, point.Latitude);
                lat2 = Math.Max(lat2, point.Latitude);

                lng1 = Math.Min(lng1, point.Longitude);
                lng2 = Math.Max(lng2, point.Longitude);

            }

            return new Rectangle(lat1, lng1, lat2, lng2);

        }

        /// <summary>
        /// Returns a three-dimensional array of X and Y coordinates based on the specified <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">A two-dimensional array making up the outer and inner coordinates of a polygon.</param>
        /// <returns>A three-dimensional array of X and Y coordinates.</returns>
        public static double[][][] ToXyArray(IPoint[][] coordinates) {

            if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));

            double[][][] temp = new double[coordinates.Length][][];

            for (int i = 0; i < coordinates.Length; i++) {
                temp[i] = new double[coordinates[i].Length][];
                for (int j = 0; j < temp[i].Length; j++) {
                    temp[i][j] = new []{ coordinates[i][j].Longitude, coordinates[i][j].Latitude };
                }
            }

            return temp;

        }

    }

}