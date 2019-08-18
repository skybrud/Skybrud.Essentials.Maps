using System;
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
        /// Initializes a new rectangle based on the coordinates of the south west and north east corners respectively.
        /// </summary>
        /// <param name="lat1">The latitude of the south west corner.</param>
        /// <param name="lng1">The longitude of the south west corner.</param>
        /// <param name="lat2">The latitude of the north east corner.</param>
        /// <param name="lng2">The longitude of the north east corner.</param>
        public Rectangle(double lat1, double lng1, double lat2, double lng2) {
            SouthWest = new Point(lat1, lng1);
            NorthEast = new Point(lat2, lng2);
        }

        /// <summary>
        /// Initializes a new rectangle based on the specified <paramref name="southWest"/> and <paramref name="southWest"/>.
        /// </summary>
        /// <param name="southWest">The point that defines the south west corner of rectangle.</param>
        /// <param name="northEast">The point that defines the north east corner of the rectangle.</param>
        public Rectangle(IPoint southWest, IPoint northEast) {
            if (southWest == null) throw new ArgumentNullException(nameof(southWest));
            if (northEast == null) throw new ArgumentNullException(nameof(northEast));
            SouthWest = new Point(southWest.Latitude, northEast.Longitude);
            NorthEast = new Point(northEast.Latitude, northEast.Longitude);
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Returns whether the rectangle contains the point at the specified <paramref name="latitude"/> and <paramref name="longitude"/>.
        /// </summary>
        /// <param name="latitude">The latitude of the point.</param>
        /// <param name="longitude">The longitude of the point.</param>
        /// <returns><c>true</c>c> if the rectangle contains the point; otherwise <c>false</c>.</returns>
        public bool Contains(double latitude, double longitude) {
            return Contains(new Point(latitude, longitude));
        }

        /// <summary>
        /// Returns whether the rectangle contains <paramref name="point"/>.
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
        /// Returns an instance of <see cref="IPoint"/> representing the center of the rectangle.
        /// </summary>
        /// <returns>An instance of <see cref="IPoint"/>.</returns>
        public IPoint GetCenter() {
            double deltaLat = (North - South) / 2d;
            double deltaLng = (East - West) / 2d;
            return new Point(South + deltaLat, West + deltaLng);
        }

        /// <summary>
        /// Returns the area of the rectangle in square metres. 
        /// </summary>
        /// <returns>The area in square metres.</returns>
        public double GetArea() {

            Point southWest = new Point(SouthWest.Latitude, SouthWest.Longitude);
            Point northEast = new Point(NorthEast.Latitude, NorthEast.Longitude);
            Point southEast = new Point(SouthWest.Latitude, NorthEast.Longitude);
            Point northWest = new Point(NorthEast.Latitude, SouthWest.Longitude);

            return PolygonUtils.GetArea(new IPoint[] {northEast, northWest, southWest, southEast});

        }

        /// <summary>
        /// Returns the area of the rectangle in square metres. 
        /// </summary>
        /// <param name="radius">The radius of the spheroid.</param>
        /// <returns>The area in square metres.</returns>
        public double GetArea(double radius) {

            Point southWest = new Point(SouthWest.Latitude, SouthWest.Longitude);
            Point northEast = new Point(NorthEast.Latitude, NorthEast.Longitude);
            Point southEast = new Point(SouthWest.Latitude, NorthEast.Longitude);
            Point northWest = new Point(NorthEast.Latitude, SouthWest.Longitude);

            return PolygonUtils.GetArea(new IPoint[] {northEast, northWest, southWest, southEast}, radius);

        }

        /// <summary>
        /// Returns the circumference of the rectangle in metres. 
        /// </summary>
        /// <returns>The circumference in metres.</returns>
        public double GetCircumference() {

            double lengthX = DistanceUtils.GetDistance(SouthWest, SouthEast);
            double lengthY = DistanceUtils.GetDistance(SouthWest, NorthWest);

            return lengthX + lengthX + lengthY + lengthY;

        }

        /// <summary>
        /// Returns an instance of <see cref="IRectangle"/> representing the bounding box of the rectangle.
        /// </summary>
        /// <returns>An instance of <see cref="IRectangle"/>.</returns>
        public IRectangle GetBoundingBox() {
            return this;
        }

        #endregion

    }

}