using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Skybrud.Essentials.Collections;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Lines;
using Skybrud.Essentials.Maps.Geometry.Shapes;
using static System.Math;

namespace Skybrud.Essentials.Maps {

    /// <summary>
    /// Static class with various utility methods used throughout the package.
    /// </summary>
    public static partial class MapsUtils {

        /// <summary>
        /// Converts the specified <paramref name="radians"/> to degrees.
        /// </summary>
        /// <param name="radians">The angle specified in radians.</param>
        /// <returns>The angle in degrees.</returns>
        /// <example>
        /// The method lets you specify a decimal value for the radations, and it gives you back to corresponding value
        /// in degrees. For instance if you specify the value of <see cref="PI"/>, which defines half the distance
        /// around a circle, so it results in 180 degrees.
        /// <code class="csharp">
        /// // Returns 180
        /// double degrees = MapsUtils.RadiansToDegrees(Math.PI);
        /// </code>
        /// </example>
        public static double RadiansToDegrees(double radians) {
            return radians * (180.0 / PI);
        }

        /// <summary>
        /// Converts the specified <paramref name="degrees"/> to radians.
        /// </summary>
        /// <param name="degrees">The angle specified in degrees.</param>
        /// <returns>The angle in radians.</returns>
        /// <example>
        /// The method lets you specify a decimal value for the degrees, and gives you back to radians in return. As
        /// seen in the example below, a value of 180 degrees results in the value of <see cref="PI"/> in radians
        /// (roughly <c>3.14159</c>).
        /// <code class="csharp">
        /// // Returns PI
        /// double radians = MapsUtils.DegreesToRadians(180);
        /// </code>
        /// </example>
        public static double DegreesToRadians(double degrees) {
            return degrees * PI / 180.0;
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
        /// <exception cref="ArgumentNullException"><paramref name="origin"/> or <paramref name="destination"/> is <c>null</c>.</exception>
        public static double ComputeHeading(IPoint origin, IPoint destination) {
            if (origin == null) throw new ArgumentNullException(nameof(origin));
            if (destination == null) throw new ArgumentNullException(nameof(destination));
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
        /// <exception cref="ArgumentNullException"><paramref name="origin"/> is <c>null</c>.</exception>
        public static IPoint ComputeOffset(IPoint origin, double distance, double heading) {
            if (origin == null) throw new ArgumentNullException(nameof(origin));
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
        /// <exception cref="ArgumentNullException"><paramref name="origin"/> is <c>null</c>.</exception>
        public static IPoint ComputeOffset(IPoint origin, double distance, double heading, double radius) {

            if (origin == null) throw new ArgumentNullException(nameof(origin));

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
        /// Returns the length of the line or line string represented by the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points making up the line.</param>
        /// <returns>The length in metres.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public static double GetLength(IReadOnlyList<IPoint> points) {

            if (points == null) throw new ArgumentNullException(nameof(points));

            double sum = 0;

            // Iterate through each point in the path (skip the first point)
            for (int i = 1; i < points.Count; i++) {

                // Calculate the distance between the two points
                sum += PointUtils.GetDistance(points[i - 1], points[i]);

            }

            return sum;

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
        /// <exception cref="ArgumentNullException"><paramref name="a1"/>, <paramref name="a2"/>, <paramref name="b1"/> or <paramref name="b2"/> is <c>null</c>.</exception>
        public static IPoint? GetIntersection(IPoint a1, IPoint a2, IPoint b1, IPoint b2) {
            if (a1 == null) throw new ArgumentNullException(nameof(a1));
            if (a2 == null) throw new ArgumentNullException(nameof(a2));
            if (b1 == null) throw new ArgumentNullException(nameof(b1));
            if (b2 == null) throw new ArgumentNullException(nameof(b2));
            return GetIntersection(new Line(a1, a2), new Line(b1, b2));
        }

        /// <summary>
        /// Returns the point of intersection between <paramref name="line1"/> and <paramref name="line2"/>.
        /// </summary>
        /// <param name="line1">The first line.</param>
        /// <param name="line2">The second line.</param>
        /// <returns>An instance of <see cref="IPoint"/> representing the point of intersection, or <c>null</c> if the
        /// two lines don't intersect.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="line1"/> or <paramref name="line2"/> is <c>null</c>.</exception>
        public static IPoint? GetIntersection(ILine line1, ILine line2) {

            if (line1 == null) throw new ArgumentNullException(nameof(line1));
            if (line2 == null) throw new ArgumentNullException(nameof(line2));

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

            Point result = new(y, x);

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
        /// <exception cref="ArgumentNullException"><paramref name="a1"/>, <paramref name="a2"/>, <paramref name="b1"/> or <paramref name="b2"/> is <c>null</c>.</exception>
        public static bool Intersects(IPoint a1, IPoint a2, IPoint b1, IPoint b2) {
            if (a1 == null) throw new ArgumentNullException(nameof(a1));
            if (a2 == null) throw new ArgumentNullException(nameof(a2));
            if (b1 == null) throw new ArgumentNullException(nameof(b1));
            if (b2 == null) throw new ArgumentNullException(nameof(b2));
            return Intersects(new Line(a1, a2), new Line(b1, b2));
        }

        /// <summary>
        /// Returns whether the lines <paramref name="line1"/> and <paramref name="line2"/> intersect.
        /// </summary>
        /// <param name="line1">The first line.</param>
        /// <param name="line2">The second line.</param>
        /// <returns><c>true</c> if the two lines intersect, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="line1"/> or <paramref name="line2"/> is <c>null</c>.</exception>
        public static bool Intersects(ILine line1, ILine line2) {
            if (line1 == null) throw new ArgumentNullException(nameof(line1));
            if (line2 == null) throw new ArgumentNullException(nameof(line2));
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
        /// <exception cref="ArgumentNullException"><paramref name="a1"/>, <paramref name="a2"/>, <paramref name="b1"/> or <paramref name="b2"/> is <c>null</c>.</exception>
        public static bool Intersects(IPoint a1, IPoint a2, IPoint b1, IPoint b2, [NotNullWhen(true)] out IPoint? result) {
            if (a1 == null) throw new ArgumentNullException(nameof(a1));
            if (a2 == null) throw new ArgumentNullException(nameof(a2));
            if (b1 == null) throw new ArgumentNullException(nameof(b1));
            if (b2 == null) throw new ArgumentNullException(nameof(b2));
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
        /// <exception cref="ArgumentNullException"><paramref name="line1"/> or <paramref name="line2"/> is <c>null</c>.</exception>
        public static bool Intersects(ILine line1, ILine line2, [NotNullWhen(true)] out IPoint? result) {
            if (line1 == null) throw new ArgumentNullException(nameof(line1));
            if (line2 == null) throw new ArgumentNullException(nameof(line2));
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
        /// <exception cref="ArgumentNullException"><paramref name="line"/> is <c>null</c>.</exception>
        public static IPoint GetCenter(ILineString line) {

            if (line == null) throw new ArgumentNullException(nameof(line));

            double distance = GetLength(line.Points) / 2d;

            for (int i = 1; i < line.Points.Count; i++) {

                IPoint a = line.Points[i - 1];
                IPoint b = line.Points[i];

                double d = PointUtils.GetDistance(a, b);

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

        /// <summary>
        /// Returns the center point of the specified collection of <paramref name="points"/>.
        /// </summary>
        /// <param name="points">An array of points to find the center for.</param>
        /// <returns>An instance of <see cref="IPoint"/> representing the center point.</returns>
        /// <remarks>Internally the method will find the bounding box of the coordinates, and then get the center of
        /// the bounding box. For one, this means that the center is not weighed - eg. if a majority of the coordinates
        /// are in close proximity to eachother, it won't affect the result.</remarks>
        public static IPoint GetCenter(IEnumerable<IPoint> points) {
            if (points == null) throw new ArgumentNullException(nameof(points));
            return GetBoundingBox(points).GetCenter();
        }

        /// <summary>
        /// Returns the length of the line or line string represented by the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points making up the line.</param>
        /// <returns>The length in metres.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public static double GetLength(IPoint[] points) {

            if (points == null) throw new ArgumentNullException(nameof(points));

            double sum = 0;

            // Iterate through each point in the path (skip the first point)
            for (int i = 1; i < points.Length; i++) {

                // Calculate the distance between the two points
                sum += PointUtils.GetDistance(points[i - 1], points[i]);

            }

            return sum;

        }

        /// <summary>
        /// Returns an array with the X and Y coordinates of the specified <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The point to bee converted.</param>
        /// <returns>An array of <see cref="double"/> representing the coordinates of <paramref name="point"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="point"/> is <c>null</c>.</exception>
        public static double[] ToXyArray(IPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            return new[] {point.Longitude, point.Latitude};
        }

        /// <summary>
        /// Returns a two-dimensional array of X and Y coordinates based on the specified array <paramref name="points"/>.
        /// </summary>
        /// <param name="points">An array of points to be converted.</param>
        /// <returns>A three-dimensional array of X and Y coordinates.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public static double[][] ToXyArray(IPoint[] points) {

            if (points == null) throw new ArgumentNullException(nameof(points));

            double[][] temp = ArrayUtils.Empty<double[]>();

            for (int i = 0; i < points.Length; i++) {
                temp[i] = ToXyArray(points[i]);
            }

            return temp;

        }

        /// <summary>
        /// Returns a three-dimensional array of X and Y coordinates based on the specified array <paramref name="points"/>.
        /// </summary>
        /// <param name="points">A two-dimensional array making up the outer and inner coordinates of a polygon.</param>
        /// <returns>A three-dimensional array of X and Y coordinates.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public static double[][][] ToXyArray(IPoint[][] points) {

            if (points == null) throw new ArgumentNullException(nameof(points));

            double[][][] temp = new double[points.Length][][];

            for (int i = 0; i < points.Length; i++) {
                temp[i] = new double[points[i].Length][];
                for (int j = 0; j < temp[i].Length; j++) {
                    temp[i][j] = new []{ points[i][j].Longitude, points[i][j].Latitude };
                }
            }

            return temp;

        }

        /// <summary>
        /// Returns a two-dimensional array representing the points of the specified <paramref name="polygon"/>.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <returns>A two-dimensional array of <see cref="IPoint"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="polygon"/> is <c>null</c>.</exception>
        public static IReadOnlyList<IReadOnlyList<IPoint>> GetCoordinates(IPolygon polygon) {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            List<IReadOnlyList<IPoint>> temp = new() { polygon.Outer };
            temp.AddRange(polygon.Inner);
            return temp.ToArray();
        }

        /// <summary>
        /// Returns the area of the polygon in square metres.
        /// </summary>
        /// <param name="points">The points making up the polygon.</param>
        /// <returns>A <see cref="double"/> representing the area in square metres.</returns>
        /// <see>
        ///     <cref>https://developers.google.com/maps/documentation/javascript/reference/3/geometry#spherical.computeArea</cref>
        /// </see>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public static double GetArea(IPoint[] points) {
            if (points == null) throw new ArgumentNullException(nameof(points));
            return GetArea(points, EarthConstants.EquatorialRadius);
        }

        /// <summary>
        /// Returns the area of the polygon in square metres.
        /// </summary>
        /// <param name="points">The points making up the polygon.</param>
        /// <returns>A <see cref="double"/> representing the area in square metres.</returns>
        /// <see>
        ///     <cref>https://developers.google.com/maps/documentation/javascript/reference/3/geometry#spherical.computeArea</cref>
        /// </see>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public static double GetArea(IEnumerable<IPoint> points) {
            if (points == null) throw new ArgumentNullException(nameof(points));
            return GetArea(points.ToArray(), EarthConstants.EquatorialRadius);
        }

        /// <summary>
        /// Returns the area of the polygon in square metres.
        /// </summary>
        /// <param name="points">The points making up the polygon.</param>
        /// <param name="radius">The radius of the spheroid.</param>
        /// <returns>A <see cref="double"/> representing the area in square metres.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public static double GetArea(IEnumerable<IPoint> points, double radius) {
            if (points == null) throw new ArgumentNullException(nameof(points));
            return GetArea(points.ToArray(), radius);
        }

        /// <summary>
        /// Returns the area of the polygon in square metres.
        /// </summary>
        /// <param name="points">The points making up the polygon.</param>
        /// <param name="radius">The radius of the spheroid.</param>
        /// <returns>A <see cref="double"/> representing the area in square metres.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public static double GetArea(IPoint[] points, double radius) {

            if (points == null) throw new ArgumentNullException(nameof(points));

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
        /// Returns the circumference of the closed path represented by the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points making up the closed path.</param>
        /// <returns>The circumference in metres.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public static double GetCircumference(IPoint[] points) {

            if (points == null) throw new ArgumentNullException(nameof(points));

            double sum = 0;

            // Iterate through each point in the path
            for (int i = 0; i < points.Length; i++) {

                // While "i" is the index of the first point and "j" is the second point
                int j = i == 0 ? points.Length - 1 : i - 1;

                // Calculate the distance between the two points
                sum += PointUtils.GetDistance(points[i], points[j]);

            }

            return sum;

        }

        /// <summary>
        /// Returns the circumference of the closed path represented by the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points making up the closed path.</param>
        /// <returns>The circumference in metres.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public static double GetCircumference(IEnumerable<IPoint> points) {
            if (points == null) throw new ArgumentNullException(nameof(points));
            return GetCircumference(points.ToArray());
        }

        /// <summary>
        /// Returns the circumference of the closed path represented by the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points making up the closed path.</param>
        /// <param name="radius">The radius of the spheroid.</param>
        /// <returns>The circumference in metres.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public static double GetCircumference(IPoint[] points, double radius) {

            if (points == null) throw new ArgumentNullException(nameof(points));

            double sum = 0;

            // Iterate through each point in the path
            for (int i = 0; i < points.Length; i++) {

                // While "i" is the index of the first point and "j" is the second point
                int j = i == 0 ? points.Length - 1 : i - 1;

                // Calculate the distance between the two points
                sum += PointUtils.GetDistance(points[i], points[j], radius);

            }

            return sum;

        }

        /// <summary>
        /// Returns the circumference of the closed path represented by the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points making up the closed path.</param>
        /// <param name="radius">The radius of the spheroid.</param>
        /// <returns>The circumference in metres.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public static double GetCircumference(IEnumerable<IPoint> points, double radius) {
            if (points == null) throw new ArgumentNullException(nameof(points));
            return GetCircumference(points.ToArray(), radius);
        }

        /// <summary>
        /// Returns whether the closed path defined by the specified <paramref name="coordinates"/> contains <paramref name="point"/>.
        /// </summary>
        /// <param name="coordinates">The coordinates of the closed path.</param>
        /// <param name="point">The point.</param>
        /// <returns><c>true</c> if the closed path contains <paramref name="point"/>, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="coordinates"/> or <paramref name="point"/> is <c>null</c>.</exception>
        public static bool Contains(IPoint[] coordinates, IPoint point) {

            if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));
            if (point == null) throw new ArgumentNullException(nameof(point));

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
        /// Returns whether the closed path defined by the specified <paramref name="coordinates"/> contains <paramref name="point"/>.
        /// </summary>
        /// <param name="coordinates">The coordinates of the closed path.</param>
        /// <param name="point">The point.</param>
        /// <returns><c>true</c> if the closed path contains <paramref name="point"/>, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="coordinates"/> or <paramref name="point"/> is <c>null</c>.</exception>
        public static bool Contains(IEnumerable<IPoint> coordinates, IPoint point) {
            if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));
            if (point == null) throw new ArgumentNullException(nameof(point));
            return Contains(coordinates.ToArray(), point);
        }

        /// <summary>
        /// Returns an instance of <see cref="IRectangle"/> representing the bounding box containing all of the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns>An instance of <see cref="IRectangle"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public static IRectangle GetBoundingBox(IEnumerable<IPoint> points) {

            if (points == null) throw new ArgumentNullException(nameof(points));

            IPoint[] array = points.ToArray();

            double lat1 = array[0].Latitude;
            double lat2 = array[0].Latitude;

            double lng1 = array[0].Longitude;
            double lng2 = array[0].Longitude;

            foreach (IPoint point in array) {

                lat1 = Min(lat1, point.Latitude);
                lat2 = Max(lat2, point.Latitude);

                lng1 = Min(lng1, point.Longitude);
                lng2 = Max(lng2, point.Longitude);

            }

            return new Rectangle(lat1, lng1, lat2, lng2);

        }

    }

}