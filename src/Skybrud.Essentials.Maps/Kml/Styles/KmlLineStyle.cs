using System.Xml;
using System.Xml.Linq;

namespace Skybrud.Essentials.Maps.Kml.Styles {

    /// <summary>
    /// Specifies the drawing style (color, color mode, and line width) for all line geometry. Line geometry includes
    /// the outlines of outlined polygons and the extruded "tether" of Placemark icons (if extrusion is enabled).
    /// </summary>
    /// <see>
    ///     <cref>https://developers.google.com/kml/documentation/kmlreference#linestyle</cref>
    /// </see>
    public class KmlLineStyle : KmlColorStyle {

        #region Properties

        /// <summary>
        /// Width of the line, in pixels.
        /// </summary>
        public float Width { get; set; }

        #endregion

        #region Constructors

        public KmlLineStyle() {
            Width = 1;
        }

        protected KmlLineStyle(XElement xml, XmlNamespaceManager namespaces) : base(xml, namespaces) {
            Width = GetFloat(xml, "kml:width", namespaces, 1);
        }

        #endregion

        #region Member methods

        public override XElement ToXElement() {
            XElement xml = base.ToXElement();
            xml.Add(NewXElement("width", Width));
            return xml;
        }

        #endregion

        #region Static methods

        public static KmlLineStyle Parse(XElement xml, XmlNamespaceManager namespaces) {
            return xml == null ? null : new KmlLineStyle(xml, namespaces);
        }

        #endregion

    }

}