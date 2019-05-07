using System.Xml;
using System.Xml.Linq;
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

        public KmlPoint() {
            Coordinates = new KmlPointCoordinates();
        }

        public KmlPoint(double latitude, double longitude) {
            Coordinates = new KmlPointCoordinates(latitude, longitude);
        }

        public KmlPoint(double latitude, double longitude, double altitude) {
            Coordinates = new KmlPointCoordinates(latitude, longitude, altitude);
        }

        protected KmlPoint(XElement xml, XmlNamespaceManager namespaces) : base(xml, namespaces) {
            Coordinates = xml.GetElement("kml:coordinates", namespaces, KmlPointCoordinates.Parse) ?? new KmlPointCoordinates();
        }

        #endregion

        #region Static methods

        public override XElement ToXElement() {
            XElement xml = base.ToXElement();
            xml.Add((Coordinates ?? new KmlPointCoordinates()).ToXElement());
            return xml;
        }

        #endregion

        #region Static methods
        
        public static KmlPoint Parse(XElement xml) {
            return new KmlPoint(xml, Namespaces);
        }

        public static KmlPoint Parse(XElement xml, XmlNamespaceManager namespaces) {
            return new KmlPoint(xml, namespaces);
        }

        #endregion

    }

}