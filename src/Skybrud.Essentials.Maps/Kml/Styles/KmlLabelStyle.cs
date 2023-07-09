using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Features;

namespace Skybrud.Essentials.Maps.Kml.Styles {

    /// <summary>
    /// Specifies how the name of a <see cref="KmlFeature"/> is drawn. A custom color, color mode, and scale for the label (name) can be specified.
    /// </summary>
    /// <see>
    ///     <cref>https://developers.google.com/kml/documentation/kmlreference#labelstyle</cref>
    /// </see>
    public class KmlLabelStyle : KmlColorStyle {

        #region Properties

        /// <summary>
        /// Gets or sets the scale of the label.
        /// </summary>
        public float Scale { get; set; }

        #endregion

        #region Constructors

        public KmlLabelStyle() { }

        protected KmlLabelStyle(XElement xml, XmlNamespaceManager namespaces) : base(xml, namespaces) {
            Scale = GetFloat(xml, "kml:scale", namespaces, 1);
        }

        #endregion

        #region Member methods

        public override XElement ToXElement() {
            XElement xml = base.ToXElement();
            if (Math.Abs(Scale - 1) > Single.Epsilon) xml.Add(NewXElement("scale", Scale.ToString(CultureInfo.InvariantCulture)));
            return xml;
        }

        #endregion

        #region Static methods

        public static KmlLabelStyle Parse(XElement xml, XmlNamespaceManager namespaces) {
            return xml == null ? null : new KmlLabelStyle(xml, namespaces);
        }

        #endregion

    }

}