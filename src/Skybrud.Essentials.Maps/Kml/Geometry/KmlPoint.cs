using System;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Features;
using Skybrud.Essentials.Maps.Kml.Styles;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Geometry {

    /// <summary>
    /// A geographic location defined by longitude, latitude, and (optional) altitude. When a <see cref="KmlPoint"/> is
    /// contained by a <see cref="KmlPlacemark"/>, the point itself determines the position of the
    /// <see cref="KmlPlacemark"/>'s name and icon.
    ///
    /// When a <see cref="KmlPoint"/> is extruded, it is connected to the ground with a line. This "tether" uses the
    /// current <see cref="KmlLineStyle"/>..
    /// </summary>
    public class KmlPoint : KmlGeometry {

        #region Properties

        /// <summary>
        /// Specifies whether to connect the point to the ground with a line. To extrude a point, the value for
        /// <see cref="AltitudeMode"/> must be either relativeToGround, relativeToSeaFloor, or absolute. The point is
        /// extruded toward the center of the Earth's sphere.
        /// </summary>
        public bool Extrude { get; set; }

        /// <summary>
        /// Specifies how altitude components in the <see cref="Coordinates"/> element are interpreted.
        /// </summary>
        public KmlAltitudeMode AltitudeMode { get; set; }

        /// <summary>
        /// A single tuple consisting of floating point values for longitude, latitude, and altitude (in that order).
        /// Longitude and latitude values are in degrees, where:
        ///
        /// - <em>longitude</em> &gt;= −180 and &lt;= 180
        /// - <em>latitude</em> &gt;= −90 and &lt;= 90
        /// - altitude values (optional) are in meters above sea level
        /// </summary>
        public KmlPointCoordinates Coordinates { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty KML <c>&lt;Point&gt;</c> element.
        /// </summary>
        public KmlPoint() {
            Coordinates = new KmlPointCoordinates();
        }

        /// <summary>
        /// Initializes a new KML <c>&lt;Point&gt;</c> element based on the specified <paramref name="latitude"/> and <paramref name="longitude"/>.
        /// </summary>
        /// <param name="latitude">The latitude of the point.</param>
        /// <param name="longitude">The longitude of the point.</param>
        public KmlPoint(double latitude, double longitude) {
            Coordinates = new KmlPointCoordinates(latitude, longitude);
        }

        /// <summary>
        /// Initializes a new KML <c>&lt;Point&gt;</c> element based on the specified <paramref name="latitude"/>, <paramref name="longitude"/> and <paramref name="altitude"/>.
        /// </summary>
        /// <param name="latitude">The latitude of the point.</param>
        /// <param name="longitude">The longitude of the point.</param>
        /// <param name="altitude">The altitude of the point.</param>
        public KmlPoint(double latitude, double longitude, double altitude) {
            Coordinates = new KmlPointCoordinates(latitude, longitude, altitude);
        }

        /// <summary>
        /// Initializes a new KML <c>&lt;Point&gt;</c> element.
        /// </summary>
        /// <param name="xml">The XML element the document should be based on.</param>
        /// <param name="namespaces">The XML namespace.</param>
        protected KmlPoint(XElement xml, IXmlNamespaceResolver namespaces) : base(xml) {
            Coordinates = xml.GetElement("kml:coordinates", namespaces, KmlPointCoordinates.Parse) ?? new KmlPointCoordinates();
        }

        #endregion

        #region Static methods

        /// <inheritdoc />
        public override XElement ToXElement() {
            XElement xml = base.ToXElement();
            xml.Add(Coordinates.ToXElement());
            return xml;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlPoint"/>.
        /// </summary>
        /// <param name="xml">The XML element representing the document.</param>
        /// <returns>An instance of <see cref="KmlPoint"/>.</returns>
        public static KmlPoint Parse(XElement xml) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return new KmlPoint(xml, Namespaces);
        }

        /// <summary>
        /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlPoint"/>.
        /// </summary>
        /// <param name="xml">The XML element representing the document.</param>
        /// <param name="namespaces">The XML namespace.</param>
        /// <returns>An instance of <see cref="KmlPoint"/>.</returns>
        public static KmlPoint Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return new KmlPoint(xml, namespaces ?? Namespaces);
        }

        #endregion

    }

}