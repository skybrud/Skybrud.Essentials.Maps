using System.Xml;
using System.Xml.Linq;

namespace Skybrud.Essentials.Maps.Kml.Features {

    public abstract class KmlContainer : KmlFeature {

        // https://developers.google.com/kml/documentation/kmlreference#container

        protected KmlContainer() { }

        protected KmlContainer(XElement xml, XmlNamespaceManager namespaces) : base(xml, namespaces) { }

    }

}