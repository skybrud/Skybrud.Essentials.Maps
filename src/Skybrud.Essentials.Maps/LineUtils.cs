using System;
using System.Collections.Generic;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Lines;
using static System.Math;
using static Skybrud.Essentials.Maps.MapsUtils;

namespace Skybrud.Essentials.Maps {

    /// <summary>
    /// Static class with various utility/helper methods for working with lines and line strings.
    /// </summary>
    public static class LineUtils {

        /// <summary>
        /// Gets the length of the line or line string represented by the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points making up the line.</param>
        /// <returns>The length in metres.</returns>
        public static double GetLength(List<IPoint> points) {
            
            double sum = 0;

            // Iterate through each point in the path (skip the first point)
            for (int i = 1; i < points.Count; i++) {

                // Calculate the distance between the two points
                sum += DistanceUtils.GetDistance(points[i - 1], points[i]);

            }

            return sum;

        }

        /// <summary>
        /// Gets the length of the line or line string represented by the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points making up the line.</param>
        /// <returns>The length in metres.</returns>
        public static double GetLength(IPoint[] points) {
            
            double sum = 0;

            // Iterate through each point in the path (skip the first point)
            for (int i = 1; i < points.Length; i++) {

                // Calculate the distance between the two points
                sum += DistanceUtils.GetDistance(points[i - 1], points[i]);

            }

            return sum;

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
        public static double ComputeHeading(double lat1, double lng1, double lat2, double lng2) {

            // Convert the coordinates from degrees to radians
            double fromLat = DegreesToRadians(lat1);
            double fromLng = DegreesToRadians(lng1);
            double toLat = DegreesToRadians(lat2);
            double toLng = DegreesToRadians(lng2);

            // Calculate the difference in longitude
            double dLng = toLng - fromLng;

            // Calculate the heading
            double heading = Atan2(
                Sin(dLng) * Cos(toLat),
                Cos(fromLat) * Sin(toLat) - Sin(fromLat) * Cos(toLat) * Cos(dLng)
            );

            // Convert the heading to degrees
            return RadiansToDegrees(heading);

        }

        /// <summary>
        /// Returns the heading of the line that goes from <paramref name="origin"/> to <paramref name="destination"/>.
        /// </summary>
        /// <param name="origin">The point of origin.</param>
        /// <param name="destination">The point of destination.</param>
        /// <returns>The heading of the line.</returns>
        public static double ComputeHeading(IPoint origin, IPoint destination) {
            return ComputeHeading(origin.Latitude, origin.Longitude, destination.Latitude, destination.Longitude);
        }

        /// <summary>
        /// Returns the <see cref="IPoint"/> resulting from moving a distance from an origin in the specified heading (expressed in degrees clockwise from north).
        ///
        /// The calculations are based on <see cref="EarthConstants.EquatorialRadius"/> as the radius.
        /// </summary>
        /// <param name="origin">The <see cref="IPoint"/> from which to start.</param>
        /// <param name="distance">The distance in metres to travel.</param>
        /// <param name="heading">The heading in degrees clockwise from north.</param>
        /// <returns>The <see cref="IPoint"/> resulting from moving a distance from an origin in the specified heading.</returns>
        public static IPoint ComputeOffset(IPoint origin, double distance, double heading) {
            return ComputeOffset(origin, distance, heading, EarthConstants.EquatorialRadius);
        }

        /// <summary>
        /// Returns the <see cref="IPoint"/> resulting from moving a distance from an origin in the specified heading (expressed in degrees clockwise from north).
        /// </summary>
        /// <param name="origin">The <see cref="IPoint"/> from which to start.</param>
        /// <param name="distance">The distance in metres to travel.</param>
        /// <param name="heading">The heading in degrees clockwise from north.</param>
        /// <param name="radius">The radius.</param>
        /// <returns>The <see cref="IPoint"/> resulting from moving a distance from an origin in the specified heading.</returns>
        public static IPoint ComputeOffset(IPoint origin, double distance, double heading, double radius) {

            distance /= radius;

            // Convert the input to radians
            heading = DegreesToRadians(heading);
            double originLat = DegreesToRadians(origin.Latitude);
            double originLng = DegreesToRadians(origin.Longitude);

            // Calculate cosinus and sinus values for the distance and latitude
            double cosDistance = Cos(distance);
            double sinDistance = Sin(distance);
            double sinOriginLat = Sin(originLat);
            double cosOriginLat = Cos(originLat);

            double sinLat = cosDistance * sinOriginLat + sinDistance * cosOriginLat * Cos(heading);
            double dLng = Atan2(sinDistance * cosOriginLat * Sin(heading), cosDistance - sinOriginLat * sinLat);
            return new Point(RadiansToDegrees(Asin(sinLat)), RadiansToDegrees(originLng + dLng));

        }

        /// <summary>
        /// Returns the point of intersections between the two lines defined by the points <paramref name="a1"/>,
        /// <paramref name="a2"/>, <paramref name="b1"/> and <paramref name="b2"/>.
        /// </summary>
        /// <param name="a1">The start point of the first line.</param>
        /// <param name="a2">The end point of the first line.</param>
        /// <param name="b1">The start point of the second line.</param>
        /// <param name="b2">The end point of the second line.</param>
        /// <returns>An instance of <see cref="IPoint"/> representing the point of intersection, or <c>null</c> if the
        /// two lines don't intersect.</returns>
        public static IPoint GetIntersection(IPoint a1, IPoint a2, IPoint b1, IPoint b2) {
            return GetIntersection(new Line(a1, a2), new Line(b1, b2));
        }

        /// <summary>
        /// Returns the point of intersection between <paramref name="line1"/> and <paramref name="line2"/>.
        /// </summary>
        /// <param name="line1">The first line.</param>
        /// <param name="line2">The second line.</param>
        /// <returns>An instance of <see cref="IPoint"/> representing the point of intersection, or <c>null</c> if the
        /// two lines don't intersect.</returns>
        public static IPoint GetIntersection(ILine line1, ILine line2) {

            // Convert "line1" to a linear equation
            double a1 = line1.B.Latitude - line1.A.Latitude;
            double b1 = line1.A.Longitude - line1.B.Longitude;
            double c1 = a1 * line1.A.Longitude + b1 * line1.A.Latitude;

            // Convert "line2" to a linear equation
            double a2 = line2.B.Latitude - line2.A.Latitude;
            double b2 = line2.A.Longitude - line2.B.Longitude;
            double c2 = a2 * line2.A.Longitude + b2 * line2.A.Latitude;

            // Calculate the determinant between the two linear equation
            double d = a1 * b2 - a2 * b1;

            // Are the lines parallel?
            if (Abs(d) < double.Epsilon) return null;

            // Calculate the X and Y coordinates
            double x = (b2 * c1 - b1 * c2) / d;
            double y = (a1 * c2 - a2 * c1) / d;

            Point result = new Point(y, x);

            // Check the boundaries of the first line
            if (result.Longitude < Min(line1.A.Longitude, line1.B.Longitude)) return null;
            if (result.Longitude > Max(line1.A.Longitude, line1.B.Longitude)) return null;
            if (result.Latitude < Min(line1.A.Latitude, line1.B.Latitude)) return null;
            if (result.Latitude > Max(line1.A.Latitude, line1.B.Latitude)) return null;

            // Check the boundaries of the second line
            if (result.Longitude < Min(line2.A.Longitude, line2.B.Longitude)) return null;
            if (result.Longitude > Max(line2.A.Longitude, line2.B.Longitude)) return null;
            if (result.Latitude < Min(line2.A.Latitude, line2.B.Latitude)) return null;
            if (result.Latitude > Max(line2.A.Latitude, line2.B.Latitude)) return null;

            return result;

        }

        /// <summary>
        /// Returns whether the two lines by the points <paramref name="a1"/>, <paramref name="a2"/>,
        /// <paramref name="b1"/> and <paramref name="b2"/> intersect.
        /// </summary>
        /// <param name="a1">The start point of the first line.</param>
        /// <param name="a2">The end point of the first line.</param>
        /// <param name="b1">The start point of the second line.</param>
        /// <param name="b2">The end point of the second line.</param>
        /// <returns><c>true</c> if the two lines intersect, otherwise <c>false</c>.</returns>
        public static bool Intersects(IPoint a1, IPoint a2, IPoint b1, IPoint b2) {
            return Intersects(new Line(a1, a2), new Line(b1, b2));
        }

        /// <summary>
        /// Returns whether the lines <paramref name="line1"/> and <paramref name="line2"/> intersect.
        /// </summary>
        /// <param name="line1">The first line.</param>
        /// <param name="line2">The second line.</param>
        /// <returns><c>true</c> if the two lines intersect, otherwise <c>false</c>.</returns>
        public static bool Intersects(ILine line1, ILine line2) {
            return GetIntersection(line1, line2) != null;
        }

        /// <summary>
        /// Returns whether the two lines by the points <paramref name="a1"/>, <paramref name="a2"/>,
        /// <paramref name="b1"/> and <paramref name="b2"/> intersect.
        /// </summary>
        /// <param name="a1">The start point of the first line.</param>
        /// <param name="a2">The end point of the first line.</param>
        /// <param name="b1">The start point of the second line.</param>
        /// <param name="b2">The end point of the second line.</param>
        /// <param name="result">An instance of <see cref="IPoint"/> representing the point of intersection, or
        /// <c>null</c> if the two points don't intersect.</param>
        /// <returns><c>true</c> if the two lines intersect, otherwise <c>false</c>.</returns>
        public static bool Intersects(IPoint a1, IPoint a2, IPoint b1, IPoint b2, out IPoint result) {
            return Intersects(new Line(a1, a2), new Line(b1, b2), out result);
        }

        /// <summary>
        /// Returns whether the lines <paramref name="line1"/> and <paramref name="line2"/> intersect.
        /// </summary>
        /// <param name="line1">The first line.</param>
        /// <param name="line2">The second line.</param>
        /// <param name="result">An instance of <see cref="IPoint"/> representing the point of intersection, or
        /// <c>null</c> if the two points don't intersect.</param>
        /// <returns><c>true</c> if the two lines intersect, otherwise <c>false</c>.</returns>
        public static bool Intersects(ILine line1, ILine line2, out IPoint result) {
            result = GetIntersection(line1, line2);
            return result != null;
        }

        /// <summary>
        /// Returns the center point of the specified <paramref name="line"/>.
        ///
        /// The center point is calculated by travelling half of the line's total length.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns>An instance of <see cref="IPoint"/> representing the center point.</returns>
        public static IPoint GetCenter(ILineString line) {

            if (line == null) throw new ArgumentNullException(nameof(line));

            double distance = GetLength(line.Points) / 2d;

            for (int i = 1; i < line.Points.Length; i++) {

                IPoint a = line.Points[i - 1];
                IPoint b = line.Points[i];

                double d = DistanceUtils.GetDistance(a, b);

                if (d > distance) {

                    // Caculate the heading between "a" and "b" (the center is on that line)
                    double heading = ComputeHeading(a, b);

                    // Calculate the offset/center based on the remaining distance
                    return ComputeOffset(a, distance, heading);

                }

                distance -= d;

            }

            return line.GetBoundingBox().GetCenter();

        }

    }

}