using System;
using System.Globalization;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Strings;

namespace Skybrud.Essentials.Maps.Kml.Geometry {

    public class KmlPointCoordinates : KmlObject, IPoint {

        #region Properties

        /// <summary>
        /// Gets or sets the latitude of the point (&gt;= −90 and &lt;= 90).
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the point (&gt;= −180 and &lt;= 180).
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the meters above sea level.
        /// </summary>
        public double Altitude { get; set; }

        public bool HasAltitude => Math.Abs(Altitude) > double.Epsilon;

        #endregion

        #region Constructors

        public KmlPointCoordinates() { }

        public KmlPointCoordinates(double latitude, double longitude) {
            Latitude = latitude;
            Longitude = longitude;
        } 

        public KmlPointCoordinates(double latitude, double longitude, double altitude) {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
        }

        public KmlPointCoordinates(double[] array) {
            Latitude = array[1];
            Longitude = array[0];
            Altitude = array.Length == 3 ? array[2] : 0;
        }

        protected KmlPointCoordinates(XElement xml) {

            double[] array = StringUtils.ParseDoubleArray(xml.Value);

            Latitude = array[1];
            Longitude = array[0];
            Altitude = array.Length == 3 ? array[2] : 0;

        }

        #endregion

        #region Member methods

        public override string ToString() {

            // Determine whether the altitude should be included
            string format = HasAltitude ? "{0},{1},{2}" : "{0},{1}";

            // Generate the string value
            return string.Format(CultureInfo.InvariantCulture, format, Longitude, Latitude, Altitude);

        }

        public override XElement ToXElement() {
            return NewXElement("coordinates",ToString());
        }

        #endregion

        #region Static methods

        public static KmlPointCoordinates Parse(XElement xml) {
            return xml == null ? null : new KmlPointCoordinates(xml);
        }

        #endregion

    }

}