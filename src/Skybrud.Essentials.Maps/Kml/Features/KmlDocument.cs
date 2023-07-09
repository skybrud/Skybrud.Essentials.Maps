using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Geometry;
using Skybrud.Essentials.Maps.Kml.Styles;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Features {

    /// <summary>
    /// Class representing a KML <c>&lt;Document&gt;</c> element.
    /// </summary>
    public class KmlDocument : KmlContainer {

        #region Properties

        /// <summary>
        /// Gets or sets the <c>&lt;NetworkLink&gt;</c> element of the document.
        /// </summary>
        public KmlNetworkLink? NetworkLink { get; set; }

        /// <summary>
        /// Gets whether the document has a <c>&lt;NetworkLink&gt;</c> element.
        /// </summary>
        [MemberNotNullWhen(true, "NetworkLink")]
        public bool HasNetworkLink => NetworkLink is not null;

        /// <summary>
        /// Gets the style collection of the document.
        /// </summary>
        public KmlStyleSelectorCollection StyleSelectors { get; }

        /// <summary>
        /// Gets a reference to the features collection of the document.
        /// </summary>
        public KmlFeatureCollection Features { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty KML <c>&lt;Document&gt;</c> element.
        /// </summary>
        public KmlDocument() {
            StyleSelectors = new KmlStyleSelectorCollection();
            Features = new KmlFeatureCollection();
        }

        /// <summary>
        /// Initializes a new KML <c>&lt;Document&gt;</c> element containing the specified <paramref name="features"/>.
        /// </summary>
        /// <param name="features">An array of features to add to the document.</param>
        public KmlDocument(params KmlFeature[] features) {
            StyleSelectors = new KmlStyleSelectorCollection();
            Features = features;
        }

        /// <summary>
        /// Initializes a new KML <c>&lt;Document&gt;</c> element.
        /// </summary>
        /// <param name="xml">The XML element the document should be based on.</param>
        /// <param name="namespaces">The XML namespace.</param>
        protected KmlDocument(XElement xml, IXmlNamespaceResolver namespaces) : base(xml, namespaces) {

            NetworkLink = xml.GetElement("kml:NetworkLink", namespaces, x => KmlNetworkLink.Parse(x, namespaces));

            List<KmlStyleSelector> selectors = new();

            List<KmlFeature> features = new();

            foreach (XElement child in xml.Elements()) {

                switch (child.Name.LocalName) {

                    case "Style":
                        selectors.Add(KmlStyle.Parse(child, namespaces));
                        break;

                    case "StyleMap":
                        selectors.Add(KmlStyleMap.Parse(child, namespaces));
                        break;

                    case "Document":
                        features.Add(Parse(child, namespaces));
                        break;

                    case "Folder":
                        features.Add(KmlFolder.Parse(child, namespaces));
                        break;

                    case "NetworkLink":
                        features.Add(KmlNetworkLink.Parse(child, namespaces));
                        break;

                    case "Placemark":
                        features.Add(KmlPlacemark.Parse(child, namespaces));
                        break;

                    case "GroundOverlay":
                    case "PhotoOverlay":
                    case "ScreenOverlay":
                        // currently not supported
                        break;

                }

            }

            StyleSelectors = new KmlStyleSelectorCollection(selectors);
            Features = new KmlFeatureCollection(features);

        }

        #endregion

        #region Member methods

        /// <inheritdoc />
        public override XElement ToXElement() {

            XElement xml = base.ToXElement();

            if (NetworkLink is not null) {
                xml.Add(NetworkLink.ToXElement());
            }

            foreach (KmlStyleSelector selector in StyleSelectors) {
                xml.Add(selector.ToXElement());
            }

            foreach (KmlFeature feature in Features) {
                xml.Add(feature.ToXElement());
            }

            return xml;

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlDocument"/>.
        /// </summary>
        /// <param name="xml">The XML element representing the document.</param>
        /// <returns>An instance of <see cref="KmlDocument"/>.</returns>
        public static KmlDocument Parse(XElement xml) {
            return new KmlDocument(xml, Namespaces);
        }

        /// <summary>
        /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlDocument"/>.
        /// </summary>
        /// <param name="xml">The XML element representing the document.</param>
        /// <param name="namespaces">The XML namespace.</param>
        /// <returns>An instance of <see cref="KmlDocument"/>.</returns>
        public static KmlDocument Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
            return new KmlDocument(xml, namespaces ?? Namespaces);
        }

        #endregion

    }

}