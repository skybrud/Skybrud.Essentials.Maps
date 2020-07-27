using System;
using Skybrud.Essentials.Maps.Geometry;

namespace Skybrud.Essentials.Maps.GeoJson {

    /// <summary>
    /// Class representing a set of X and Y coordinates.
    /// </summary>
    public class GeoJsonCoordinates {

        #region Properties

        /// <summary>
        /// Gets or sets the coordinate on the X-axis.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the coordinate on the X-axis.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the altitude.
        /// </summary>
        public double Altitude { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance with default properties.
        /// </summary>
        public GeoJsonCoordinates() { }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="x"/> and <paramref name="y"/> coordinates.
        /// </summary>
        /// <param name="x">The coordinate on the X-axis.</param>
        /// <param name="y">The coordinate on the Y-axis.</param>
        public GeoJsonCoordinates(double x, double y) {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="x"/> and <paramref name="y"/> coordinates and <paramref name="altitude"/>.
        /// </summary>
        /// <param name="x">The coordinate on the X-axis.</param>
        /// <param name="y">The coordinate on the Y-axis.</param>
        /// <param name="altitude">The altitude.</param>
        public GeoJsonCoordinates(double x, double y, double altitude) {
            X = x;
            Y = y;
            Altitude = altitude;
        }

        /// <summary>
        /// Initializes a new instance based on the specified array of <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">An array with the coordinates.</param>
        public GeoJsonCoordinates(double[] coordinates) {
            X = coordinates[0];
            Y = coordinates[1];
            Altitude = coordinates.Length == 3 ? coordinates[2] : 0;
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The point.</param>
        public GeoJsonCoordinates(IPoint point) {
            X = point.Longitude;
            Y = point.Latitude;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Converts the coordinates to an array of <see cref="double"/>.
        ///
        /// If a value for <see cref="Altitude"/> is present, the array will contain values for <see cref="X"/>,
        /// <see cref="Y"/> and <see cref="Altitude"/> respectively. If <see cref="Altitude"/> is not present, the
        /// array will only include values for <see cref="X"/> and <see cref="Y"/>.
        /// </summary>
        /// <returns>An array of <see cref="double"/>.</returns>
        public double[] ToArray() {
            return Math.Abs(Altitude) < double.Epsilon ? new[] { X, Y } : new[] { X, Y, Altitude };
        }

        /// <summary>
        /// Converts the coordinates to an instance of <see cref="IPoint"/>.
        /// </summary>
        /// <returns>An instance of <see cref="IPoint"/>.</returns>
        public IPoint ToPoint() {
            return new Point(Y, X);
        }

        #endregion

    }

}