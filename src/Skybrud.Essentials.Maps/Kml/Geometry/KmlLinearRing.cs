﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Common;
using Skybrud.Essentials.Strings.Extensions;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Geometry;

/// <summary>
/// Defines a closed line string, typically the outer boundary of a polygon. Optionally, a
/// <c>&lt;LinearRing&gt;</c> can also be used as the inner boundary of a polygon to create holes in the polygon.
/// A oolygon can contain multiple <c>&lt;LinearRing&gt;</c> elements used as inner boundaries.
/// </summary>
/// <see>
///     <cref>https://developers.google.com/kml/documentation/kmlreference#linearring</cref>
/// </see>
public class KmlLinearRing : KmlGeometry {

    #region Properties

    /// <summary>
    /// Specifies whether to connect the <see cref="KmlLinearRing"/> to the ground. To extrude this geometry, the
    /// altitude mode must be either <see cref="KmlAltitudeMode.RelativeToGround"/> or
    /// <see cref="KmlAltitudeMode.Absolute"/>. Only the vertices of the <see cref="KmlLinearRing"/> are extruded,
    /// not the center of the geometry. The vertices are extruded toward the center of the Earth's sphere.
    /// </summary>
    public bool Extrude { get; set; }

    /// <summary>
    /// Specifies whether to allow the <see cref="KmlLinearRing"/> to follow the terrain. To enable tessellation,
    /// the value for <see cref="AltitudeMode"/> must be <see cref="KmlAltitudeMode.ClampToGround"/>. Very large
    /// linear rings should enable tessellation so that they follow the curvature of the earth (otherwise, they may
    /// go underground and be hidden).
    /// </summary>
    public bool Tesselate { get; set; }

    /// <summary>
    /// Specifies how <em>altitude</em> components in the &lt;coordinates&gt; element are interpreted.
    /// </summary>
    public KmlAltitudeMode AltitudeMode { get; set; }

    /// <summary>
    /// Four or more tuples, each consisting of floating point values for <em>longitude</em>, <em>latitude</em>,
    /// and <em>altitude</em>. The <em>altitude</em> component is optional.
    ///
    /// The last coordinate must be the same as the first coordinate. Coordinates are expressed in decimal degrees only.
    /// </summary>
    public KmlLinearRingCoordinates Coordinates { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new empty KML <c>&lt;Document&gt;</c> element.
    /// </summary>
    public KmlLinearRing() {
        Coordinates = new KmlLinearRingCoordinates();
    }

    /// <summary>
    /// Initializes a new KML <c>&lt;Document&gt;</c> element containing the specified <paramref name="coordinates"/>.
    /// </summary>
    /// <param name="coordinates">An array of coordinates that make up the linear ring.</param>
    public KmlLinearRing(params KmlPointCoordinates[] coordinates) {
        Coordinates = new KmlLinearRingCoordinates(coordinates);
    }

    /// <summary>
    /// Initializes a new KML <c>&lt;Document&gt;</c> element containing the specified <paramref name="coordinates"/>.
    /// </summary>
    /// <param name="coordinates">An array of coordinates that make up the linear ring.</param>
    public KmlLinearRing(IEnumerable<KmlPointCoordinates> coordinates) {
        Coordinates = new KmlLinearRingCoordinates(coordinates);
    }

    /// <summary>
    /// Initializes a new KML <c>&lt;Document&gt;</c> element containing the specified <paramref name="coordinates"/>.
    /// </summary>
    /// <param name="coordinates">An array of coordinates that make up the linear ring.</param>
    public KmlLinearRing(double[][] coordinates) {
        Coordinates = new KmlLinearRingCoordinates(coordinates);
    }

    /// <summary>
    /// Initializes a new KML <c>&lt;Document&gt;</c> element.
    /// </summary>
    /// <param name="xml">The XML element the document should be based on.</param>
    /// <param name="namespaces">The XML namespace.</param>
    protected KmlLinearRing(XElement xml, IXmlNamespaceResolver namespaces) {
        Extrude = xml.GetElementValueAsBoolean("kml:extrude", namespaces);
        Tesselate = xml.GetElementValueAsBoolean("kml:tesselate", namespaces);
        Coordinates = xml.GetElement("kml:coordinates", namespaces, KmlLinearRingCoordinates.Parse) ?? new KmlLinearRingCoordinates();
    }

    #endregion

    #region Member methods

    /// <inheritdoc />
    public override XElement ToXElement() {

        if (Coordinates == null) throw new PropertyNotSetException(nameof(Coordinates));

        XElement xml = base.ToXElement();

        if (Extrude) xml.Add(NewXElement("extrude", "1"));
        if (Tesselate) xml.Add(NewXElement("tessellate", "1"));
        if (AltitudeMode != default) xml.Add(NewXElement("altitudeMode", AltitudeMode.ToCamelCase()));

        xml.Add(Coordinates.ToXElement());

        return xml;

    }

    #endregion

    #region Static methods

    /// <summary>
    /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlLinearRing"/>.
    /// </summary>
    /// <param name="xml">The XML element representing the document.</param>
    /// <returns>An instance of <see cref="KmlLinearRing"/>.</returns>
    public static KmlLinearRing Parse(XElement xml) {
        if (xml is null) throw new ArgumentNullException(nameof(xml));
        return new KmlLinearRing(xml, Namespaces);
    }

    /// <summary>
    /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlLinearRing"/>.
    /// </summary>
    /// <param name="xml">The XML element representing the document.</param>
    /// <param name="namespaces">The XML namespace.</param>
    /// <returns>An instance of <see cref="KmlLinearRing"/>.</returns>
    public static KmlLinearRing Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
        if (xml is null) throw new ArgumentNullException(nameof(xml));
        return new KmlLinearRing(xml, namespaces ?? Namespaces);
    }

    #endregion

}