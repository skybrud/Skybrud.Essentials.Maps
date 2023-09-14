using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Geometry;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Features;

/// <summary>
/// Class representing a KML <c>&lt;Placemark&gt;</c> element.
/// </summary>
public class KmlPlacemark : KmlFeature {

    #region Properties

    /// <summary>
    /// Gets or sets the extended data of the placemark.
    /// </summary>
    public KmlExtendedData? ExtendedData { get; set; }

    /// <summary>
    /// Gets or sets the geometry of the placemark.
    /// </summary>
    public KmlGeometry Geometry { get; set; } // TODO: Should this be nullable?

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new KML <c>&lt;Placemark&gt;</c> element containing the specified <paramref name="geometry"/>..
    /// </summary>
    /// <param name="geometry">The geometry to be added.</param>
    public KmlPlacemark(KmlGeometry geometry) {
        Geometry = geometry;
    }

    /// <summary>
    /// Initializes a new KML <c>&lt;Document&gt;</c> element.
    /// </summary>
    /// <param name="xml">The XML element the document should be based on.</param>
    protected KmlPlacemark(XElement xml) : this(xml, Namespaces) { }

    /// <summary>
    /// Initializes a new KML <c>&lt;Document&gt;</c> element.
    /// </summary>
    /// <param name="xml">The XML element the document should be based on.</param>
    /// <param name="namespaces">The XML namespace.</param>
    protected KmlPlacemark(XElement xml, IXmlNamespaceResolver namespaces) : base(xml, namespaces) {
        ExtendedData = xml.GetElement("kml:ExtendedData", namespaces, KmlExtendedData.Parse);
        Geometry = KmlUtils.ParseGeometryChildren(xml, namespaces).Single();
    }

    #endregion

    #region Member methods

    /// <inheritdoc />
    public override XElement ToXElement() {

        if (Geometry is null) throw new ArgumentNullException(nameof(Geometry));

        XElement xml = base.ToXElement();

        xml.Add(Geometry.ToXElement());

        return xml;

    }

    #endregion

    #region Static methods

    /// <summary>
    /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlPlacemark"/>.
    /// </summary>
    /// <param name="xml">The XML element representing the placemark.</param>
    /// <returns>An instance of <see cref="KmlPlacemark"/>.</returns>
    public static KmlPlacemark Parse(XElement xml) {
        if (xml is null) throw new ArgumentNullException(nameof(xml));
        return new KmlPlacemark(xml, Namespaces);
    }

    /// <summary>
    /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlPlacemark"/>.
    /// </summary>
    /// <param name="xml">The XML element representing the placemark.</param>
    /// <param name="namespaces">The XML namespace.</param>
    /// <returns>An instance of <see cref="KmlPlacemark"/>.</returns>
    public static KmlPlacemark Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
        if (xml is null) throw new ArgumentNullException(nameof(xml));
        return new KmlPlacemark(xml, namespaces ?? Namespaces);
    }

    #endregion

}