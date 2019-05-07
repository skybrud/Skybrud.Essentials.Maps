using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Strings.Extensions;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Styles {
    
    /// <summary>
    /// This is an abstract element and cannot be used directly in a KML file. It provides elements for specifying the color and color mode of extended style types.
    /// </summary>
    /// <see>
    ///     <cref>https://developers.google.com/kml/documentation/kmlreference#style</cref>
    /// </see>
    public abstract class KmlColorStyle : KmlObject {

        #region Properties

        public string Id { get; set; }

        /// <summary>
        /// Color and opacity (alpha) values are expressed in hexadecimal notation. The range of values for any one
        /// color is 0 to 255 (<c>00</c> to <c>ff</c>). For alpha, <c>00</c> is fully transparent and <c>ff</c> is
        /// fully opaque. The order of expression is <em>aabbggrr</em>, where <em>aa=alpha</em> (00 to ff);
        /// <em>bb=blue</em> (00 to ff); <em>gg=green</em> (00 to ff); <em>rr=red</em> (00 to ff). For example, if you
        /// want to apply a blue color with 50 percent opacity to an overlay, you would specify the following:
        /// <c>&lt;color&gt;7fff0000&lt;/color&gt;</c>, where <em>alpha</em>=0x7f, <em>blue</em>=0xff,
        /// <em>green</em>=0x00, and <em>red</em>=0x00.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Values for <see cref="ColorMode"/> are <see cref="KmlColorMode.Normal"/> (no effect) and
        /// <see cref="KmlColorMode.Random"/>. A value of <see cref="KmlColorMode.Random"/> applies a random linear
        /// scale to the base <see cref="Color"/> as follows:
        /// 
        /// - To achieve a truly random selection of colors, specify a base &lt;color&gt; of white (ffffffff).
        /// - If you specify a single color component (for example, a value of ff0000ff for red), random color values for that one component (red) will be selected. In this case, the values would range from 00 (black) to ff (full red).
        /// - If you specify values for two or for all three color components, a random linear scale is applied to each color component, with results ranging from black to the maximum values specified for each component.
        /// - The opacity of a color comes from the alpha component of &lt;color&gt; and is never randomized.
        /// </summary>
        public KmlColorMode ColorMode { get; set; }

        #endregion

        #region Constructors

        protected KmlColorStyle() { }

        protected KmlColorStyle(string color) {
            Color = color;
        }

        protected KmlColorStyle(XElement xml, XmlNamespaceManager namespaces) {
            Id = xml.GetAttributeValue("id");
            Color = xml.GetElementValue("kml:color", namespaces);
            ColorMode = xml.GetElementValueAsEnum("kml:colorMode", namespaces, KmlColorMode.Normal);
        }

        #endregion
        
        #region Member methods
        
        public override XElement ToXElement() {

            XElement xml = base.ToXElement();

            if (Id.HasValue()) xml.Add(new XAttribute("id", Id));

            if (Color.HasValue()) xml.Add(NewXElement("color", Color));
            if (ColorMode != KmlColorMode.Normal) xml.Add(NewXElement("colorMode", ColorMode.ToLower()));

            return xml;

        }

        #endregion

    }

}