using System;
using System.Collections.Generic;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps {

    public static class MapsUtils {

        /// <summary>
        /// Gets a two-dimensional representing the points of the specified <paramref name="polygon"/>.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <returns>A two-dimensional array of <see cref="IPoint"/>.</returns>
        public static IPoint[][] GetCoordinates(IPolygon polygon) {
            List<IPoint[]> temp = new List<IPoint[]>();
            temp.Add(polygon.Outer);
            temp.AddRange(polygon.Inner);
            return temp.ToArray();
        }

        /// <summary>
        /// Returns the heading from one point (identified by <paramref name="lat1"/> and <paramref name="lng1"/>) to
        /// another (indentified by <paramref name="lat2"/> and <paramref name="lng2"/>).
        /// </summary>
        /// <param name="lat1">The latitude of the origin point.</param>
        /// <param name="lng1">The longitude of the origin point.</param>
        /// <param name="lat2">The latitude of the destination point.</param>
        /// <param name="lng2">The longitude of the destination point.</param>
        /// <returns>The heading in degrees clockwise from north.</returns>
        /// <see>
        ///     <cref>https://gis.stackexchange.com/a/228663</cref>
        /// </see>
        public static double GetHeading(double lat1, double lng1, double lat2, double lng2) {

            // Convert the coordinates from degrees to radians
            double fromLat = DegreesToRadians(lat1);
            double fromLng = DegreesToRadians(lng1);
            double toLat = DegreesToRadians(lat2);
            double toLng = DegreesToRadians(lng2);

            // Calculate the difference in longitude
            double dLng = toLng - fromLng;

            // Calculate the heading
            double heading = Math.Atan2(
                Math.Sin(dLng) * Math.Cos(toLat),
                Math.Cos(fromLat) * Math.Sin(toLat) - Math.Sin(fromLat) * Math.Cos(toLat) * Math.Cos(dLng)
            );

            // Convert the heading to degrees
            return RadiansToDegrees(heading);

        }















        /// <summary>
        /// Gets the length of the polyline represented by the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points making up the line.</param>
        /// <returns>The length in metres.</returns>
        public static double GetLength(List<IPoint> points) {
            
            double sum = 0;

            // Iterate through each point in the path (skip the first point)
            for (int i = 1; i < points.Count; i++) {

                // Calculate the distance between the two points
                sum += DistanceUtils.GetDistance(points[i - i], points[i]);

            }

            return sum;

        }

        /// <summary>
        /// Gets the length of the polyline represented by the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points making up the line.</param>
        /// <returns>The length in metres.</returns>
        public static double GetLength(IPoint[] points) {
            
            double sum = 0;

            // Iterate through each point in the path (skip the first point)
            for (int i = 1; i < points.Length; i++) {

                // Calculate the distance between the two points
                sum += DistanceUtils.GetDistance(points[i - i], points[i]);

            }

            return sum;

        }

        /// <summary>
        /// Gets the area of the polygon in square metres. 
        /// </summary>
        /// <returns>Returns a <see cref="System.Double"/> representing the area in square metres.</returns>
        /// <see>
        ///     <cref>https://developers.google.com/maps/documentation/javascript/reference/3/geometry#spherical.computeArea</cref>
        /// </see>
        public static double GetArea(IPoint[] points) {
            return GetArea(points, EarthConstants.EquatorialRadius);
        }

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
        /// Gets the circumference of the closed path represented by the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points making up the closed path.</param>
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
        /// Converts the specified <paramref name="radians"/> to degrees.
        /// </summary>
        /// <param name="radians">The angle specified in radians.</param>
        /// <returns>The angle in degrees.</returns>
        public static double RadiansToDegrees(double radians) {
            return radians * (180.0 / Math.PI);
        }

        /// <summary>
        /// Converts the specified <paramref name="degrees"/> to radians.
        /// </summary>
        /// <param name="degrees">The angle specified in degrees.</param>
        /// <returns>The angle in radians.</returns>
        public static double DegreesToRadians(double degrees) {
            return degrees * Math.PI / 180.0;
        }










        public static bool IsPointInPolygon(IPoint[] coordinates, IPoint point) {

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