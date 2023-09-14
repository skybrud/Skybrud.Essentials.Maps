using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Exceptions;
using Skybrud.Essentials.Maps.Kml.Extensions;
using Skybrud.Essentials.Maps.Kml.Features;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml;

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

    public KmlNetworkLinkControl? NetworkLinkControl { get; set; }

#if NET7_0_OR_GREATER
        public required KmlFeature Feature { get; set; }
# else
    public KmlFeature Feature { get; set; }
#endif

    /// <summary>
    /// Alias of <see cref="Feature"/>, but with the type <see cref="KmlDocument"/>.
    /// </summary>
    public KmlDocument Document {
        get => (KmlDocument) Feature;
        set => Feature = value;
    }

    #endregion

    #region Constructors

#if NET7_0_OR_GREATER
        public KmlFile() { }
#endif

#if NET7_0_OR_GREATER
        [SetsRequiredMembers]
#endif
    public KmlFile(KmlFeature feature) {
        Feature = feature;
    }

#if NET7_0_OR_GREATER
        [SetsRequiredMembers]
#endif
    protected KmlFile(XElement xml, IXmlNamespaceResolver namespaces) {
        NetworkLinkControl = xml.GetElement("kml:NetworkLinkControl", namespaces, x => KmlNetworkLinkControl.Parse(x, namespaces));
        Feature = xml.GetElement("kml:Document", namespaces, x => KmlDocument.Parse(x, namespaces))!;
        if (Feature is null) throw new KmlParseException($"Failed parsing 'kml:Document' from '{xml.Name}'...");
    }

    #endregion

    #region Member methods

    protected void ParseInternal(string xml) {
        ParseInternal(XElement.Parse(xml), Namespaces);
    }

    protected void ParseInternal(XElement xml, IXmlNamespaceResolver namespaces) {
        Document = xml.GetElement("kml:Document", namespaces, x => KmlDocument.Parse(x, namespaces))!;
        if (Document is null) throw new KmlParseException($"Failed parsing 'kml:Document' from '{xml.Name}'...");
    }

    public override XElement ToXElement() {
        XElement xml = NewXElement("kml");
        if (NetworkLinkControl is not null) xml.Add(NetworkLinkControl.ToXElement());
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

    public static KmlFile Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
        return new KmlFile(xml, namespaces ?? Namespaces);
    }

    public static KmlFile Parse(XDocument xml) {
        if (xml.Root is null) throw new KmlParseException("XDocument doesn't specify a root element.");
        return new KmlFile(xml.Root, Namespaces);
    }

    public static KmlFile Parse(XDocument xml, IXmlNamespaceResolver? namespaces) {
        if (xml.Root is null) throw new KmlParseException("XDocument doesn't specify a root element.");
        return new KmlFile(xml.Root, namespaces ?? Namespaces);
    }

    #endregion

}