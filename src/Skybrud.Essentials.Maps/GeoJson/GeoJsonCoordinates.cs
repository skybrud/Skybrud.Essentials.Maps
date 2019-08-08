using System;
using Skybrud.Essentials.Maps.Geometry;

namespace Skybrud.Essentials.Maps.GeoJson {

    public class GeoJsonCoordinates {

        #region Properties

        public double X { get; set; }

        public double Y { get; set; }

        public double Altitude { get; set; }

        #endregion

        #region Constructors

        public GeoJsonCoordinates() { }

        public GeoJsonCoordinates(double x, double y) {
            X = x;
            Y = y;
        }

        public GeoJsonCoordinates(double x, double y, double altitude) {
            X = x;
            Y = y;
            Altitude = altitude;
        }

        public GeoJsonCoordinates(double[] coordinates) {
            X = coordinates[0];
            Y = coordinates[1];
            Altitude = coordinates.Length == 3 ? coordinates[2] : 0;
        }

        public GeoJsonCoordinates(IPoint point) {
            X = point.Longitude;
            Y = point.Latitude;
        }

        #endregion

        #region Member methods

        public double[] ToArray() {
            return Math.Abs(Altitude) < double.Epsilon ? new[] { X, Y } : new[] { X, Y, Altitude };
        }

        public IPoint ToPoint() {
            return new Point(Y, X);
        }

        #endregion

    }

}