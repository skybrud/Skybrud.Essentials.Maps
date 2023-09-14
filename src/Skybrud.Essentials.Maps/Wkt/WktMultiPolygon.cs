using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skybrud.Essentials.Maps.Wkt.Exceptions;

namespace Skybrud.Essentials.Maps.Wkt;

/// <summary>
/// Class representing a <strong>Well Known Text</strong> multi polygon.
/// </summary>
/// <see>
///     <cref>https://en.wikipedia.org/wiki/Well-known_text_representation_of_geometry</cref>
/// </see>
public class WktMultiPolygon : WktGeometry, IReadOnlyList<WktPolygon> {

    private readonly List<WktPolygon> _polygons;

    #region Properties

    /// <summary>
    /// Gets the number of polygons in this multi polygon.
    /// </summary>
    public int Count => _polygons.Count;

    /// <summary>
    /// Gets the <see cref="WktPolygon"/> at the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>The <see cref="WktPolygon"/> at the specified index.</returns>
    public WktPolygon this[int index] => _polygons[index];

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes an empty multi polygon geometry.
    /// </summary>
    public WktMultiPolygon() {
        _polygons = new List<WktPolygon>();
    }

    /// <summary>
    /// Initializes a new multi polygon geometry based on the specified array of <paramref name="polygons"/>.
    /// </summary>
    /// <param name="polygons">The polygons that should make up the multi polygon geometry.</param>
    /// <exception cref="ArgumentNullException"><paramref name="polygons"/> is <c>null</c>.</exception>
    public WktMultiPolygon(params WktPolygon[] polygons) {
        if (polygons == null) throw new ArgumentNullException(nameof(polygons));
        _polygons = new List<WktPolygon>(polygons);
    }

    /// <summary>
    /// Initializes a new multi polygon geometry based on the specified list of <paramref name="polygons"/>.
    /// </summary>
    /// <param name="polygons">The polygons that should make up the multi polygon geometry.</param>
    /// <exception cref="ArgumentNullException"><paramref name="polygons"/> is <c>null</c>.</exception>
    public WktMultiPolygon(List<WktPolygon> polygons) {
        if (polygons == null) throw new ArgumentNullException(nameof(polygons));
        _polygons = new List<WktPolygon>(polygons);
    }

    /// <summary>
    /// Initializes a new multi polygon geometry based on the specified collection of <paramref name="polygons"/>.
    /// </summary>
    /// <param name="polygons">The polygons that should make up the multi polygon geometry.</param>
    /// <exception cref="ArgumentNullException"><paramref name="polygons"/> is <c>null</c>.</exception>
    public WktMultiPolygon(IEnumerable<WktPolygon> polygons) {
        if (polygons == null) throw new ArgumentNullException(nameof(polygons));
        _polygons = new List<WktPolygon>(polygons);
    }

    #endregion

    #region Member methods

    /// <summary>
    /// Adds the specified <paramref name="polygon"/> to this multi polygon geometry.
    /// </summary>
    /// <param name="polygon">The polygon to be added.</param>
    /// <exception cref="ArgumentNullException"><paramref name="polygon"/> is <c>null</c>.</exception>
    public void Add(WktPolygon polygon) {
        if (polygon == null) throw new ArgumentNullException(nameof(polygon));
        _polygons.Add(polygon);
    }

    /// <summary>
    /// Removes the specified <paramref name="polygon"/> from this multi polygon geometry.
    /// </summary>
    /// <param name="polygon">The polygon to be removed.</param>
    /// <exception cref="ArgumentNullException"><paramref name="polygon"/> is <c>null</c>.</exception>
    /// <returns><c>true</c> if <paramref name="polygon"/> is successfully removed; otherwise, <c>false</c>.</returns>
    public bool Remove(WktPolygon polygon) {
        if (polygon == null) throw new ArgumentNullException(nameof(polygon));
        return _polygons.Remove(polygon);
    }

    /// <inheritdoc />
    public IEnumerator<WktPolygon> GetEnumerator() {
        return _polygons.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    /// <summary>
    /// Returns the <strong>Well Known Text</strong> string representation of this multi polygon geometry.
    /// </summary>
    /// <returns>The line string formatted as a <strong>Well Known Text</strong> string.</returns>
    public override string ToString() {
        return ToString(WktFormatting.Default);
    }

    /// <summary>
    /// Returns the <strong>Well Known Text</strong> string representation of this multi polygon geometry.
    /// </summary>
    /// <param name="formatting">The formatting to be used.</param>
    /// <returns>The line string formatted as a <strong>Well Known Text</strong> string.</returns>
    public string ToString(WktFormatting formatting) {

        StringBuilder sb = new();

        sb.Append("MULTIPOLYGON ");

        if (_polygons.Count == 0) {

            sb.Append("EMPTY");

        } else {

            sb.Append("(" + string.Join(formatting == WktFormatting.Minified ? "," : ", ", from polygon in _polygons select ToString(polygon, formatting, 1)) + ")");

        }

        return sb.ToString();

    }

    #endregion

    #region Static methods

    /// <summary>
    /// Parses the specified <paramref name="input"/> string to an instance of <see cref="WktMultiPolygon"/>.
    /// </summary>
    /// <param name="input">The input string to parse.</param>
    /// <returns>An instance of <seealso cref="WktMultiPolygon"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="input"/> is <c>null</c>.</exception>
    /// <exception cref="WktInvalidFormatException"><paramref name="input"/> is not in a known format.</exception>
    public static new WktMultiPolygon Parse(string input) {

        if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException(nameof(input));

        input = input.Trim();

        if (!input.StartsWith("MULTIPOLYGON")) throw new WktInvalidFormatException(input);

        if (input.Equals("MULTIPOLYGON EMPTY")) return new WktMultiPolygon();

        input = input.Substring(12).Trim().Replace("\n", " ");
        input = input.Substring(1, input.Length - 2);

        List<WktPolygon> polygons = new List<WktPolygon>();

        string temp = "";

        int c = 0;

        for (int i = 0; i < input.Length; i++) {

            char chr = input[i];

            if (chr == '(') {
                c++;
                temp += chr;
            } else if (chr == ')') {
                c--;
                temp += chr;
                if (c == 0) {
                    polygons.Add(WktPolygon.Parse(temp));
                    temp = "";
                }
            } else if (chr == ',' && c == 0) {

            } else {
                temp += chr;
            }

        }

        return new WktMultiPolygon(polygons);

    }

    #endregion

}