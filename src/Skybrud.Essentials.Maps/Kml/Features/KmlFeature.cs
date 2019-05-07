using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Strings.Extensions;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Features {

    /// <summary>
    /// This is an abstract element and cannot be used directly in a KML file.
    /// </summary>
    /// <see>
    ///     <cref>https://developers.google.com/kml/documentation/kmlreference#feature</cref>
    /// </see>
    public abstract class KmlFeature : KmlObject {

        #region Properties

        public string Id { get; set; }

        /// <summary>
        /// User-defined text displayed in the 3D viewer as the label for the object (for example, for a <see cref="KmlPlacemark"/>, <see cref="KmlFolder"/>, or <see cref="KmlNetworkLinkControl"/>).
        /// </summary>
        public string Name { get; set; }

        public string Description { get; set; }

        public string StyleUrl { get; set; }

        #endregion

        #region Constructors

        protected KmlFeature() { }

        protected KmlFeature(XElement xml, XmlNamespaceManager namespaces) {
            Name = xml.GetElementValue("kml:name", namespaces);
            Description = xml.GetElementValue("kml:description", namespaces);
            StyleUrl = xml.GetElementValue("kml:styleUrl", namespaces);
        }

        #endregion

        #region Member methods

        public override XElement ToXElement() {

            XElement xml = base.ToXElement();

            if (Id.HasValue()) xml.Add(new XAttribute("id", Id));

            if (Name.HasValue()) xml.Add(NewXElement("name", Name));
            if (Description.HasValue()) xml.Add(NewXElement("description", Description));
            if (StyleUrl.HasValue()) xml.Add(NewXElement("styleUrl", StyleUrl));

            return xml;

        }

        #endregion

    }

}