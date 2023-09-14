using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Exceptions;
using Skybrud.Essentials.Strings.Extensions;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml;

/// <see>
///     <cref>https://developers.google.com/kml/documentation/kmlreference#link</cref>
/// </see>
public class KmlLink : KmlObject {

    #region Properties

    /// <summary>
    /// Gets or sets a URL (either an HTTP address or a local file specification).
    /// </summary>
#if NET7_0_OR_GREATER
        public required string Href { get; set; }
#else
    public string Href { get; set; }
#endif

    /// <summary>
    /// Gets or sets the refresh mode of the link.
    /// </summary>
    public KmlLinkRefreshMode RefreshMode { get; set; }

    /// <summary>
    /// Indicates to refresh the file every <em>n</em> seconds.
    /// </summary>
    public TimeSpan RefreshInterval { get; set; }

    #endregion

    #region Constructors

#if NET7_0_OR_GREATER
        public KmlLink() { }
#endif

#if NET7_0_OR_GREATER
        [SetsRequiredMembers]
#endif
    protected KmlLink(XElement xml, IXmlNamespaceResolver namespaces) {
        Href = xml.GetElementValue("kml:href", namespaces);
        if (Href is null) throw new KmlParseException($"Failed parsing 'kml:href' from '{xml.Name}'...");
        RefreshMode = xml.GetElementValueAsEnum("kml:refreshMode", namespaces, KmlLinkRefreshMode.OnChange);
        RefreshInterval = TimeSpan.FromSeconds(xml.GetElementValueAsInt32("kml:refreshInterval", namespaces));
    }

    #endregion

    #region Member methods

    public override XElement ToXElement() {
        XElement xml = base.NewXElement("Link");
        if (Href.HasValue()) xml.Add(NewXElement("href", Href));
        if (RefreshMode != KmlLinkRefreshMode.OnChange) xml.Add(NewXElement("refreshMode", RefreshMode.ToCamelCase()));
        if (RefreshMode == KmlLinkRefreshMode.OnInterval) xml.Add(NewXElement("refreshInterval", RefreshInterval.TotalSeconds));
        return xml;
    }

    #endregion

    #region Static methods

    public static KmlLink Parse(XElement xml) {
        if (xml is null) throw new ArgumentNullException(nameof(xml));
        return new KmlLink(xml, Namespaces);
    }

    public static KmlLink Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
        if (xml is null) throw new ArgumentNullException(nameof(xml));
        return new KmlLink(xml, namespaces ?? Namespaces);
    }

    #endregion

}