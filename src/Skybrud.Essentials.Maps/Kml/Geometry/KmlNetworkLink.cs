using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Extensions;
using Skybrud.Essentials.Maps.Kml.Features;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Geometry {
    
    /// <summary>
    /// References a KML file or KMZ archive on a local or remote network. Use the <see cref="KmlLink"/> element to
    /// specify the location of the KML file. Within that element, you can define the refresh options for updating the
    /// file, based on time and camera change. NetworkLinks can be used in combination with Regions to handle very
    /// large datasets efficiently.
    /// </summary>
    /// <see>
    ///     <cref>https://developers.google.com/kml/documentation/kmlreference#networklink</cref>
    /// </see>
    public class KmlNetworkLink : KmlFeature {

        #region Properties

        public bool RefreshVisibility { get; set; }

        public bool FlyToView { get; set; }

        public KmlLink Link { get; set; }

        #endregion

        #region Constructors

        public KmlNetworkLink() { }

        protected KmlNetworkLink(XElement xml, XmlNamespaceManager namespaces) : base(xml, namespaces) {
            RefreshVisibility = xml.GetElementValueAsInt32("kml:refreshVisibility", namespaces) == 1;
            FlyToView = xml.GetElementValueAsInt32("kml:flyToView", namespaces) == 1;
            Link = xml.GetElement("kml:Link", namespaces, KmlLink.Parse);
        }

        #endregion

        #region Member methods

        public override XElement ToXElement() {

            XElement xml = base.ToXElement();

            if (RefreshVisibility) xml.Add(new XElement("refreshVisibility", RefreshVisibility));
            if (FlyToView) xml.Add(new XElement("flyToView", 1));
            if (Link.HasValue()) xml.Add(Link.ToXElement());

            return xml;

        }

        #endregion

        #region Static methods
        
        public static KmlNetworkLink Parse(XElement xml) {
            return new KmlNetworkLink(xml, Namespaces);
        }

        public static KmlNetworkLink Parse(XElement xml, XmlNamespaceManager namespaces) {
            return new KmlNetworkLink(xml, namespaces);
        }

        #endregion

    }

}