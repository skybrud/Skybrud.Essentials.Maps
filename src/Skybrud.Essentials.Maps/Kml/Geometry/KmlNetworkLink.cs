using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Common;
using Skybrud.Essentials.Maps.Kml.Exceptions;
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

#if NET7_0_OR_GREATER
        public required KmlLink Link { get; set; }
#else
        public KmlLink Link { get; set; }
#endif

        #endregion

        #region Constructors

#if NET7_0_OR_GREATER
        /// <summary>
        /// Initializes a new empty KML <c>&lt;NetworkLink&gt;</c> element.
        /// </summary>
        public KmlNetworkLink() { }
#endif

        /// <summary>
        /// Initializes a new KML <c>&lt;NetworkLink&gt;</c> element.
        /// </summary>
        /// <param name="xml">The XML element the document should be based on.</param>
        /// <param name="namespaces">The XML namespace.</param>
#if NET7_0_OR_GREATER
        [SetsRequiredMembers]
#endif
        protected KmlNetworkLink(XElement xml, IXmlNamespaceResolver namespaces) : base(xml, namespaces) {
            RefreshVisibility = xml.GetElementValueAsInt32("kml:refreshVisibility", namespaces) == 1;
            FlyToView = xml.GetElementValueAsInt32("kml:flyToView", namespaces) == 1;
            Link = xml.GetElement("kml:Link", namespaces, x => KmlLink.Parse(x, namespaces))!;
            if (Link is null) throw new KmlParseException($"Failed parsing 'kml:Link' from '{xml.Name}'...");
        }

#endregion

#region Member methods

        /// <inheritdoc />
        public override XElement ToXElement() {

            if (Link is null) throw new PropertyNotSetException(nameof(Link));

            XElement xml = base.ToXElement();

            if (RefreshVisibility) xml.Add(new XElement("refreshVisibility", RefreshVisibility));
            if (FlyToView) xml.Add(new XElement("flyToView", 1));
            xml.Add(Link.ToXElement());

            return xml;

        }

#endregion

#region Static methods

        /// <summary>
        /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlNetworkLink"/>.
        /// </summary>
        /// <param name="xml">The XML element representing the document.</param>
        /// <returns>An instance of <see cref="KmlNetworkLink"/>.</returns>
        public static KmlNetworkLink Parse(XElement xml) {
            return new KmlNetworkLink(xml, Namespaces);
        }

        /// <summary>
        /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlNetworkLink"/>.
        /// </summary>
        /// <param name="xml">The XML element representing the document.</param>
        /// <param name="namespaces">The XML namespace.</param>
        /// <returns>An instance of <see cref="KmlNetworkLink"/>.</returns>
        public static KmlNetworkLink Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
            return new KmlNetworkLink(xml, namespaces ?? Namespaces);
        }

#endregion

    }

}