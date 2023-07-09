using System;
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

        protected KmlLineStyle(XElement xml, IXmlNamespaceResolver namespaces) : base(xml, namespaces) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            if (namespaces is null) throw new ArgumentNullException(nameof(namespaces));
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

        public static KmlLineStyle Parse(XElement xml) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return new KmlLineStyle(xml, Namespaces);
        }

        public static KmlLineStyle Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return new KmlLineStyle(xml, namespaces ?? Namespaces);
        }

        #endregion

    }

}