using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Features {

    /// <summary>
    /// Class representing a KML <c>&lt;Folder&gt;</c> element.
    /// </summary>
    public class KmlFolder : KmlFeature {

        #region Properties

        /// <summary>
        /// Gets a reference to the features collection of the folder.
        /// </summary>
        public KmlFeatureCollection Features { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty KML <c>&lt;Folder&gt;</c> element containing the specified <paramref name="placemarks"/>.
        /// </summary>
        /// <param name="placemarks">An array of placemarks to add to the folder.</param>
        public KmlFolder(IEnumerable<KmlPlacemark> placemarks) {
            Features = new KmlFeatureCollection(placemarks);
        }

        /// <summary>
        /// Initializes a new empty KML <c>&lt;Folder&gt;</c> element containing the specified <paramref name="features"/>.
        /// </summary>
        /// <param name="features">An array of features to add to the folder.</param>
        public KmlFolder(IEnumerable<KmlFeature> features) {
            Features = new KmlFeatureCollection(features);
        }

        /// <summary>
        /// Initializes a new empty KML <c>&lt;Folder&gt;</c> element.
        /// </summary>
        /// <param name="xml">The XML element the folder should be based on.</param>
        /// <param name="namespaces">The XML namespace.</param>
        protected KmlFolder(XElement xml, IXmlNamespaceResolver namespaces) {
            Id = xml.GetAttributeValue("id");
            Name = xml.GetElementValue("kml:name", namespaces);
            Features = xml.GetElements("kml:Placemark", namespaces, KmlPlacemark.Parse);
        }

        #endregion

        #region Member methods

        /// <inheritdoc />
        public override XElement ToXElement() {

            XElement xFolder = base.ToXElement();

            foreach (KmlFeature feature in Features) {
                xFolder.Add(feature.ToXElement());
            }

            return xFolder;

        }

        #endregion

        public static KmlFolder Parse(XElement xml) {
            return new KmlFolder(xml, Namespaces);

        }

        public static KmlFolder Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
            return new KmlFolder(xml, namespaces ?? Namespaces);
        }

    }

}