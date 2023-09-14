using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Skybrud.Essentials.Maps.Wkt.Exceptions;

namespace Skybrud.Essentials.Maps.Wkt;

/// <summary>
/// Class representing a <strong>Well Known Text</strong> multi line string.
/// </summary>
/// <see>
///     <cref>https://en.wikipedia.org/wiki/Well-known_text_representation_of_geometry</cref>
/// </see>
public class WktMultiLineString : WktGeometry, IReadOnlyList<WktLineString> {

    private readonly List<WktLineString> _lines;

    #region Properties

    /// <summary>
    /// Gets the number of line string in this multi line string.
    /// </summary>
    public int Count => _lines.Count;

    /// <summary>
    /// Gets the <see cref="WktLineString"/> at the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>The <see cref="WktLineString"/> at the specified index.</returns>
    public WktLineString this[int index] => _lines[index];

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes an empty multi line string.
    /// </summary>
    public WktMultiLineString() {
        _lines = new List<WktLineString>();
    }

    /// <summary>
    /// Initializes a new multi line string based on the specified <paramref name="lines"/>.
    /// </summary>
    /// <param name="lines">The lines that should make up the multi line string.</param>
    /// <exception cref="ArgumentNullException"><paramref name="lines"/> is <c>null</c>.</exception>
    public WktMultiLineString(IReadOnlyList<WktLineString> lines) {
        if (lines == null) throw new ArgumentNullException(nameof(lines));
        _lines = new List<WktLineString>(lines);
    }

    /// <summary>
    /// Initializes a new multi line string based on the specified <paramref name="lines"/>.
    /// </summary>
    /// <param name="lines">The lines that should make up the multi line string.</param>
    /// <exception cref="ArgumentNullException"><paramref name="lines"/> is <c>null</c>.</exception>
    public WktMultiLineString(IEnumerable<WktLineString> lines) {
        if (lines == null) throw new ArgumentNullException(nameof(lines));
        _lines = new List<WktLineString>(lines);
    }

    #endregion

    #region Member methods

    /// <summary>
    /// Adds the specified <paramref name="line"/> to this multi line string.
    /// </summary>
    /// <param name="line">The line string to be added.</param>
    /// <exception cref="ArgumentNullException"><paramref name="line"/> is <c>null</c>.</exception>
    public void Add(WktLineString line) {
        if (line == null) throw new ArgumentNullException(nameof(line));
        _lines.Add(line);
    }

    /// <summary>
    /// Removes the specified <paramref name="line"/> from this line string.
    /// </summary>
    /// <param name="line">The line string to be removed.</param>
    /// <exception cref="ArgumentNullException"><paramref name="line"/> is <c>null</c>.</exception>
    /// <returns><c>true</c> if <paramref name="line"/> is successfully removed; otherwise, <c>false</c>.</returns>
    public bool Remove(WktLineString line) {
        if (line == null) throw new ArgumentNullException(nameof(line));
        return _lines.Remove(line);
    }

    /// <inheritdoc />
    public IEnumerator<WktLineString> GetEnumerator() {
        return _lines.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    /// <summary>
    /// Returns the <strong>Well Known Text</strong> string representation of the multi line string.
    /// </summary>
    /// <returns>The multi line string formatted as a <strong>Well Known Text</strong> string.</returns>
    public override string ToString() {
        return ToString(WktFormatting.Default);
    }

    /// <summary>
    /// Returns the <strong>Well Known Text</strong> string representation of the multi line string.
    /// </summary>
    /// <param name="formatting">The formatting to be used.</param>
    /// <returns>The multi line string formatted as a <strong>Well Known Text</strong> string.</returns>
    public string ToString(WktFormatting formatting) {
        return "MULTILINESTRING " + (_lines.Count == 0 ? "EMPTY" : "(" + string.Join(", ", from line in _lines select ToString(line, formatting, 0)) + ")");
    }

    #endregion

    #region Static methods

    /// <summary>
    /// Parses the specified <paramref name="input"/> string to an instance of <see cref="WktLineString"/>.
    /// </summary>
    /// <param name="input">The input string to parse.</param>
    /// <returns>An instance of <seealso cref="WktLineString"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="input"/> is <c>null</c>.</exception>
    /// <exception cref="WktInvalidFormatException"><paramref name="input"/> is not in a known format.</exception>
    public static new WktMultiLineString Parse(string input) {

        if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException(nameof(input));

        input = input.Trim();

        if (!input.StartsWith("MULTILINESTRING")) throw new WktInvalidFormatException(input);

        if (input.Equals("MULTILINESTRING EMPTY")) return new WktMultiLineString();

        input = input.Substring(15).Trim().Replace("\n", " ");

        MatchCollection matches = Regex.Matches(input, "\\(([0-9\\., ]+)\\)");
        if (matches.Count == 0) throw new WktInvalidFormatException(input);

        return new WktMultiLineString(
            from Match match in matches
            select WktLineString.Parse(match.Groups[1].Value)
        );

    }

    #endregion

}