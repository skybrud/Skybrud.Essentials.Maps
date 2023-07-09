using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Extensions;
using Skybrud.Essentials.Maps.Kml.Features;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Styles {

    /// <summary>
    /// Specifies how icons for point <see cref="KmlPlacemark"/>'s are drawn. The <see cref="Icon"/> property specifies
    /// the icon image. The <see cref="Scale"/> property specifies the <em>x, y</em> scaling of the icon. The color
    /// specified in the <see cref="KmlColorStyle.Color"/> element of <see cref="KmlIconStyle"/> is blended with the
    /// color of <see cref="Icon"/>.
    /// </summary>
    public class KmlIconStyle : KmlColorStyle {

        #region Properties

        /// <summary>
        /// Gets or sets the scale of the icon.
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// Gets or sets the direction (that is, North, South, East, West), in degrees. Default=0 (North). Values range from 0 to 360 degrees.
        /// </summary>
        public int Heading { get; set; }

        /// <summary>
        /// Gets or sets a custom icon.
        /// </summary>
        public KmlIcon Icon { get; set; }

        #endregion

        #region Constructors

        public KmlIconStyle() {
            Scale = 1;
        }

        protected KmlIconStyle(XElement xml, XmlNamespaceManager namespaces) : base(xml, namespaces) {
            Scale = GetFloat(xml, "kml:scale", namespaces, 1);
            Icon = xml.GetElement("kml:Icon", namespaces, KmlIcon.Parse);
        }

        #endregion

        #region Member methods

        public override XElement ToXElement() {

            XElement xml = base.ToXElement();

            if (Math.Abs(Scale) > Single.Epsilon) xml.Add(NewXElement("scale", Scale.ToString(CultureInfo.InvariantCulture)));
            if (Heading != 0) xml.Add(NewXElement("heading", Heading));
            if (Icon.HasValue()) xml.Add(Icon.ToXElement());

            return xml;

        }

        #endregion

        #region Static methods

        public static KmlIconStyle Parse(XElement xml, XmlNamespaceManager namespaces) {
            return xml == null ? null : new KmlIconStyle(xml, namespaces);
        }

        #endregion

    }

}