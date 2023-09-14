using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Skybrud.Essentials.Strings;

namespace Skybrud.Essentials.Maps.Kml.Geometry;

public class KmlLinearRingCoordinates : KmlObject, IEnumerable<KmlPointCoordinates> {

    private readonly List<KmlPointCoordinates> _list;

    #region Properties

    /// <summary>
    /// Gets the amount of coordinates within the element.
    /// </summary>
    public int Count => _list.Count;

    /// <summary>
    /// Gets the coordinates at the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>An instance of <see cref="KmlPointCoordinates"/>.</returns>
    public KmlPointCoordinates this[int index] => _list.ElementAt(index);

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new empty KML <c>&lt;coordinates&gt;</c> element.
    /// </summary>
    public KmlLinearRingCoordinates() {
        _list = new List<KmlPointCoordinates>();
    }

    /// <summary>
    /// Initializes a new KML <c>&lt;coordinates&gt;</c> element based on the specified <paramref name="coordinates"/>.
    /// </summary>
    /// <param name="coordinates">An array of coordinates that make up the element.</param>
    public KmlLinearRingCoordinates(double[][] coordinates) {
        if (coordinates is null) throw new ArgumentNullException(nameof(coordinates));
        _list = new List<KmlPointCoordinates>();
        foreach (double[] item in coordinates) {
            _list.Add(new KmlPointCoordinates(item));
        }
    }

    /// <summary>
    /// Initializes a new KML <c>&lt;coordinates&gt;</c> element based on the specified <paramref name="coordinates"/>.
    /// </summary>
    /// <param name="coordinates">An array of coordinates that make up the element.</param>
    public KmlLinearRingCoordinates(params KmlPointCoordinates[] coordinates) {
        if (coordinates is null) throw new ArgumentNullException(nameof(coordinates));
        _list = new List<KmlPointCoordinates>();
    }

    /// <summary>
    /// Initializes a new KML <c>&lt;coordinates&gt;</c> element based on the specified <paramref name="coordinates"/>.
    /// </summary>
    /// <param name="coordinates">An array of coordinates that make up the element.</param>
    public KmlLinearRingCoordinates(IEnumerable<KmlPointCoordinates> coordinates) {
        if (coordinates is null) throw new ArgumentNullException(nameof(coordinates));
        _list = new List<KmlPointCoordinates>();
    }

    /// <summary>
    /// Initializes a new KML <c>&lt;coordinates&gt;</c> element.
    /// </summary>
    /// <param name="xml">The XML element the document should be based on.</param>
    public KmlLinearRingCoordinates(XElement xml) {
        if (xml is null) throw new ArgumentNullException(nameof(xml));
        string[] value = xml.Value.Split(new []{ " ", "\n", "\r", "\t" }, StringSplitOptions.RemoveEmptyEntries);
        _list = value.Select(x => new KmlPointCoordinates(StringUtils.ParseDoubleArray(x))).ToList();
    }

    #endregion

    #region Member methods

    /// <summary>
    /// Adds the specified <paramref name="coordinates"/>.
    /// </summary>
    /// <param name="coordinates">The coordinates to be added.</param>
    public void Add(KmlPointCoordinates coordinates) {
        _list.Add(coordinates);
    }

    /// <inheritdoc />
    public override XElement ToXElement() {
        string value = string.Join(" ", from p in _list select p.ToString());
        return NewXElement("coordinates", value);
    }

    /// <inheritdoc />
    public IEnumerator<KmlPointCoordinates> GetEnumerator() {
        return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    #endregion

    #region Static methods

    /// <summary>
    /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlLinearRingCoordinates"/>.
    /// </summary>
    /// <param name="xml">The XML element representing the document.</param>
    /// <returns>An instance of <see cref="KmlLinearRingCoordinates"/>.</returns>
    public static KmlLinearRingCoordinates Parse(XElement xml) {
        if (xml == null) throw new ArgumentNullException(nameof(xml));
        return new KmlLinearRingCoordinates(xml);
    }

    #endregion

}