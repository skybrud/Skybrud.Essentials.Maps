using System.IO;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Extensions;
using Skybrud.Essentials.Maps.Kml.Features;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml {

    /// <summary>
    /// The root element of a KML file. This element is required. It follows the xml declaration at the beginning of the file.
    ///
    /// The <c>&lt;kmlgt;</c> element may also include the namespace for any external XML schemas that are referenced within the file.
    ///
    /// A basic <c>&lt;kmlgt;</c> element contains 0 or 1 <see cref="KmlFeature"/> and 0 or 1 <see cref="KmlNetworkLinkControl"/>.
    /// </summary>
    /// <see>
    ///     <cref>https://developers.google.com/kml/documentation/kmlreference#kml</cref>
    /// </see>
    public class KmlFile : KmlObject {

        #region Properties

        public KmlNetworkLinkControl NetworkLinkControl { get; set; }

        public KmlFeature Feature { get; set; }

        /// <summary>
        /// Alias of <see cref="Feature"/>, but with the type <see cref="KmlDocument"/>.
        /// </summary>
        public KmlDocument Document {
            get => Feature as KmlDocument;
            set => Feature = value;
        }

        #endregion

        #region Constructors

        public KmlFile() { }

        public KmlFile(KmlFeature feature) {
            Feature = feature;
        }

        protected KmlFile(XElement xml, XmlNamespaceManager namespaces) {
            NetworkLinkControl = xml.GetElement("kml:NetworkLinkControl", namespaces, KmlNetworkLinkControl.Parse);
            Document = xml.GetElement("kml:Document", namespaces, KmlDocument.Parse);
        }

        #endregion

        #region Member methods

        protected void ParseInternal(string xml) {
            ParseInternal(XElement.Parse(xml), Namespaces);
        }

        protected void ParseInternal(XElement xml, XmlNamespaceManager namespaces) {
            Document = xml.GetElement("kml:Document", namespaces, x => KmlDocument.Parse(x, namespaces));
        }

        public override XElement ToXElement() {
            XElement xml = NewXElement("kml");
            if (NetworkLinkControl.HasValue()) xml.Add(NetworkLinkControl.ToXElement());
            if (Feature.HasValue()) xml.Add(Feature.ToXElement());
            return xml;
        }

        public XDocument ToXDocument() {
            return new XDocument(
                new XDeclaration("1.0", "UTF-8", null),
                ToXElement()
            );
        }

        public void Save(string path) {
            ToXDocument().Save(path);
        }

        public void Save(Stream stream) {
            ToXDocument().Save(stream);
        }

        public void Save(Stream stream, SaveOptions options) {
            ToXDocument().Save(stream, options);
        }

        public void Save(TextWriter writer) {
            ToXDocument().Save(writer);
        }

        public void Save(TextWriter writer, SaveOptions options) {
            ToXDocument().Save(writer, options);
        }

        public void Save(XmlWriter writer) {
            ToXDocument().Save(writer);
        }

        public void WriteTo(XmlWriter writer) {
            ToXDocument().WriteTo(writer);
        }

        #endregion

        #region Static methods

        public static KmlFile Load(string path) {
            return Parse(XElement.Load(path));
        }

        public static KmlFile Parse(string xml) {
            return Parse(XDocument.Parse(xml));
        }

        public static KmlFile Parse(XElement xml) {
            return new KmlFile(xml, Namespaces);
        }

        public static KmlFile Parse(XElement xml, XmlNamespaceManager namespaces) {
            return new KmlFile(xml, namespaces);
        }

        public static KmlFile Parse(XDocument xml) {
            return new KmlFile(xml.Root, Namespaces);
        }

        public static KmlFile Parse(XDocument xml, XmlNamespaceManager namespaces) {
            return new KmlFile(xml.Root, namespaces);
        }

        #endregion

    }

}