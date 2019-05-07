using System;
using System.Collections.Generic;
using System.Linq;
using Skybrud.Essentials.Common;

namespace Skybrud.Essentials.Maps.Geometry.Shapes {
    
    /// <summary>
    /// Clas representing a retangle.
    /// </summary>
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/Rectangle</cref>
    /// </see>
    public class Rectangle : IRectangle {

        #region Properties

        /// <summary>
        /// Gets or sets the south west point.
        /// </summary>
        public IPoint SouthWest { get; }

        /// <summary>
        /// Gets or sets the north east point.
        /// </summary>
        public IPoint NorthEast { get; }

        public double North => NorthEast.Latitude;

        public double East => NorthEast.Longitude;

        public double South => SouthWest.Latitude;

        public double West => SouthWest.Longitude;

        public IPoint SouthEast => new Point(South, East);

        public IPoint NorthWest => new Point(North, West);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance with default options.
        /// </summary>
        public Rectangle() {
            SouthWest = new Point();
            NorthEast = new Point();
        }

        /// <summary>
        /// Initializes a new rectangle based on the specified <paramref name="point1"/> and <paramref name="point1"/>.
        /// </summary>
        /// <param name="point1">The first point.</param>
        /// <param name="point2">The second point.</param>
        public Rectangle(IPoint point1, IPoint point2) {

            if (point1 == null) throw new ArgumentNullException(nameof(point1));
            if (point2 == null) throw new ArgumentNullException(nameof(point2));

            double lat1 = Math.Min(point1.Latitude, point2.Latitude);
            double lon1 = Math.Min(point1.Longitude, point2.Longitude);

            double lat2 = Math.Max(point1.Latitude, point2.Latitude);
            double lon2 = Math.Max(point1.Longitude, point2.Longitude);

            SouthWest = new Point(lat1, lon1);
            NorthEast = new Point(lat2, lon2);

        }

        /// <summary>
        /// Initializes a new rectangle representing the bounding box of the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points the rectangle should be based on.</param>
        public Rectangle(params IPoint[] points) {

            if (points == null) throw new ArgumentNullException(nameof(points));

            double lat1 = points[0].Latitude;
            double lat2 = points[0].Latitude;

            double lng1 = points[0].Longitude;
            double lng2 = points[0].Longitude;

            foreach (IPoint point in points) {

                lat1 = Math.Min(lat1, point.Latitude);
                lat2 = Math.Max(lat2, point.Latitude);

                lng1 = Math.Min(lng1, point.Longitude);
                lng2 = Math.Max(lng2, point.Longitude);

            }

            SouthWest = new Point(lat1, lng1);
            NorthEast = new Point(lat2, lng2);

        }

        /// <summary>
        /// Initializes a new rectangle representing the bounding box of the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points the rectangle should be based on.</param>
        public Rectangle(IEnumerable<IPoint> points) {

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

            SouthWest = new Point(lat1, lng1);
            NorthEast = new Point(lat2, lng2);

        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets whether the rectangle contains the point at the specified <paramref name="latitude"/> and <paramref name="longitude"/>.
        /// </summary>
        /// <param name="latitude">The latitude of the point.</param>
        /// <param name="longitude">The longitude of the point.</param>
        /// <returns><c>true</c>c> if the rectangle contains the point; otherwise <c>false</c>.</returns>
        public bool Contains(double latitude, double longitude) {
            return Contains(new Point(latitude, longitude));
        }

        /// <summary>
        /// Gets whether the rectangle contains <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns><c>true</c>c> if the rectangle contains the point; otherwise <c>false</c>.</returns>
        public bool Contains(IPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            if (SouthWest == null) throw new PropertyNotSetException(nameof(SouthWest));
            if (NorthEast == null) throw new PropertyNotSetException(nameof(NorthEast));
            if (point.Latitude < SouthWest.Latitude) return false;
            if (point.Latitude > NorthEast.Latitude) return false;
            if (point.Longitude < SouthWest.Longitude) return false;
            if (point.Longitude > NorthEast.Longitude) return false;
            return true;
        }

        /// <summary>
        /// Gets an instance of <see cref="IPoint"/> representing the center of the rectangle.
        /// </summary>
        /// <returns>An instance of <see cref="IPoint"/>.</returns>
        public IPoint GetCenter() {
            double deltaLat = (North - South) / 2d;
            double deltaLng = (East - West) / 2d;
            return new Point(South + deltaLat, West + deltaLng);
        }

        /// <summary>
        /// Gets the area of the rectangle in square metres. 
        /// </summary>
        /// <returns>The area in square metres.</returns>
        public double GetArea() {

            Point southWest = new Point(SouthWest.Latitude, SouthWest.Longitude);
            Point northEast = new Point(NorthEast.Latitude, NorthEast.Longitude);
            Point southEast = new Point(SouthWest.Latitude, NorthEast.Longitude);
            Point northWest = new Point(NorthEast.Latitude, SouthWest.Longitude);

            return MapsUtils.GetArea(new IPoint[] {northEast, northWest, southWest, southEast});

        }
        public double GetArea(double radius) {

            Point southWest = new Point(SouthWest.Latitude, SouthWest.Longitude);
            Point northEast = new Point(NorthEast.Latitude, NorthEast.Longitude);
            Point southEast = new Point(SouthWest.Latitude, NorthEast.Longitude);
            Point northWest = new Point(NorthEast.Latitude, SouthWest.Longitude);

            return MapsUtils.GetArea(new IPoint[] {northEast, northWest, southWest, southEast}, radius);

        }

        /// <summary>
        /// Gets the circumference of the rectangle in metres. 
        /// </summary>
        /// <returns>The circumference in metres.</returns>
        public double GetCircumference() {

            double lengthX = DistanceUtils.GetDistance(SouthWest, SouthEast);
            double lengthY = DistanceUtils.GetDistance(SouthWest, NorthWest);

            return lengthX + lengthX + lengthY + lengthY;

        }

        /// <summary>
        /// Gets an instance of <see cref="IRectangle"/> representing the bounding box of the rectangle.
        /// </summary>
        /// <returns>An instance of <see cref="IRectangle"/>.</returns>
        public IRectangle GetBoundingBox() {
            return this;
        }

        #endregion

        #region Static methods

        public static Rectangle GetFromPolygon(Polygon polygon) {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            return new Rectangle(polygon.Outer);
        }

        #endregion

    }

}