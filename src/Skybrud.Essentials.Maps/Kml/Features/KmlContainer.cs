using System.Xml;
using System.Xml.Linq;

namespace Skybrud.Essentials.Maps.Kml.Features {

    /// <summary>
    /// Class representing a KML container type.
    /// </summary>
    public abstract class KmlContainer : KmlFeature {

        // https://developers.google.com/kml/documentation/kmlreference#container

        /// <summary>
        /// Initializes a new empty KML container.
        /// </summary>
        protected KmlContainer() { }

        /// <summary>
        /// Initializes a new KML container.
        /// </summary>
        /// <param name="xml">The XML element the container should be based on.</param>
        /// <param name="namespaces">The XML namespace.</param>
        protected KmlContainer(XElement xml, XmlNamespaceManager namespaces) : base(xml, namespaces) { }

    }

}