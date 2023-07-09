using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Extensions;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Styles {

    /// <summary>
    /// Specifies how the description balloon for placemarks is drawn. The <c>&lt;bgColor&gt;</c>, if specified, is used as the background color of the balloon.
    /// </summary>
    public class KmlBalloonStyle : KmlColorStyle {

        #region Properties

        /// <summary>
        /// Gets or sets the background color of the balloon (optional). Color and opacity (alpha) values are expressed
        /// in hexadecimal notation.
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the foreground color for text. The default is black (<code>ff000000</code>).
        /// </summary>
        public string TextColor { get; set; }

        /// <summary>
        /// Gets or sets the text displayed in the balloon. If no text is specified, Google Earth draws the default
        /// balloon (with the Feature <c>&lt;name&gt;</c> in boldface, the Feature <c>&lt;description&gt;</c>, links
        /// for driving directions, a white background, and a tail that is attached to the point coordinates of the
        /// Feature, if specified).
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// If set to is <c>default</c>, Google Earth uses the information supplied in <see cref="Text"/> to create a
        /// balloon. If set to <c>hide</c>, Google Earth does not display the balloon. In Google Earth, clicking the
        /// List View icon for a Placemark whose balloon's <see cref="DisplayMode"/> is <c>hide</c> causes Google Earth
        /// to fly to the Placemark.
        /// </summary>
        public string DisplayMode { get; set; }

        #endregion

        #region Constructors

        public KmlBalloonStyle() { }

        protected KmlBalloonStyle(XElement xml, XmlNamespaceManager namespaces) : base(xml, namespaces) {
            BackgroundColor = xml.GetElementValue("kml:bgColor", namespaces);
            TextColor = xml.GetElementValue("kml:textColor", namespaces);
            Text = xml.GetElementValue("kml:text", namespaces);
            DisplayMode = xml.GetElementValue("kml:displayMode", namespaces);
        }

        #endregion

        #region Member methods

        public override XElement ToXElement() {

            XElement xml = base.ToXElement();

            if (BackgroundColor.HasValue()) xml.Add(NewXElement("bgColor", BackgroundColor));
            if (TextColor.HasValue()) xml.Add(NewXElement("textColor", TextColor));
            if (Text.HasValue()) xml.Add(NewXElement("text", new XCData(Text)));
            if (DisplayMode.HasValue()) xml.Add(NewXElement("displayMode", DisplayMode));

            return xml;

        }

        #endregion

        #region Static methods

        public static KmlBalloonStyle Parse(XElement xml, XmlNamespaceManager namespaces) {
            return xml == null ? null : new KmlBalloonStyle(xml, namespaces);
        }

        #endregion

    }

}