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

        /// <summary>
        /// Gets or sets the ID of the feature.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User-defined text displayed in the 3D viewer as the label for the object (for example, for a <see cref="KmlPlacemark"/>, <see cref="KmlFolder"/>, or <see cref="KmlNetworkLinkControl"/>).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the feature.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// URL of a &lt;Style&gt; or &lt;StyleMap&gt; defined in a Document. If the style is in the same file, use a #
        /// reference. If the style is defined in an external file, use a full URL along with # referencing.
        /// </summary>
        public string StyleUrl { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new KML feature.
        /// </summary>
        protected KmlFeature() { }

        /// <summary>
        /// Initializes a new KML feature.
        /// </summary>
        /// <param name="xml">The XML element the feature should be based on.</param>
        /// <param name="namespaces">The XML namespace.</param>
        protected KmlFeature(XElement xml, XmlNamespaceManager namespaces) {
            Name = xml.GetElementValue("kml:name", namespaces);
            Description = xml.GetElementValue("kml:description", namespaces);
            StyleUrl = xml.GetElementValue("kml:styleUrl", namespaces);
        }

        #endregion

        #region Member methods

        /// <inheritdoc />
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