using System;
using System.Xml;
using System.Xml.Linq;

namespace Skybrud.Essentials.Maps.Kml.Styles {

    /// <summary>
    /// Specifies the drawing style for all polygons, including polygon extrusions (which look like the walls of buildings) and line extrusions (which look like solid fences).
    /// </summary>
    /// <see>
    ///     <cref>https://developers.google.com/kml/documentation/kmlreference#polystyle</cref>
    /// </see>
    public class KmlPolyStyle : KmlColorStyle {

        #region Properties

        /// <summary>
        /// Specifies whether to fill the polygon.
        /// </summary>
        public bool Fill { get; set; }

        /// <summary>
        /// Specifies whether to outline the polygon. Polygon outlines use the current <see cref="KmlLineStyle"/>.
        /// </summary>
        public bool Outline { get; set; }

        #endregion

        #region Constructors

        public KmlPolyStyle() {
            Fill = true;
            Outline = true;
        }

        protected KmlPolyStyle(XElement xml, IXmlNamespaceResolver namespaces) : base(xml, namespaces) {
            Fill = GetBoolean(xml, "kml:fill", namespaces, true);
            Outline = GetBoolean(xml, "kml:outline", namespaces, true);
        }

        #endregion

        #region Member methods

        public override XElement ToXElement() {
            XElement xml = base.ToXElement();
            xml.Add(NewXElement("fill", Fill ? 1 : 0));
            xml.Add(NewXElement("outline", Outline ? 1 : 0));
            return xml;
        }

        #endregion

        #region Static methods

        public static KmlPolyStyle Parse(XElement xml) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return new KmlPolyStyle(xml, Namespaces);
        }

        public static KmlPolyStyle Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return new KmlPolyStyle(xml, namespaces ?? Namespaces);
        }

        #endregion

    }

}