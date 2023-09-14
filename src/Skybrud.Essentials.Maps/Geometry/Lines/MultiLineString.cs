using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.Geometry.Lines;

/// <summary>
/// Class representing a collection of line strings (<see cref="ILineString"/>).
/// </summary>
public class MultiLineString : IMultiLineString {

    private readonly List<ILineString> _lineStrings;

    #region Properties

    /// <summary>
    /// Gets the number of <see cref="ILineString"/> in this multi line string.
    /// </summary>
    public int Count => _lineStrings.Count;

    /// <summary>
    /// Gets the <see cref="ILineString"/> at the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>The <see cref="ILineString"/> at the specified index.</returns>
    public ILineString this[int index] => _lineStrings[index];

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new, empty multi line string.
    /// </summary>
    public MultiLineString() {
        _lineStrings = new List<ILineString>();
    }

    /// <summary>
    /// Initializes a new multi line string based on the specified <paramref name="collection"/> of line strings.
    /// </summary>
    /// <param name="collection">A collection of line strings.</param>
    public MultiLineString(IEnumerable<ILineString> collection) {
        _lineStrings = new List<ILineString>(collection);
    }

    #endregion

    #region Member methods

    /// <inheritdoc />
    public double GetLength() {
        if (_lineStrings.Count == 0) throw new InvalidOperationException("This MultiLineString does not contain any LineString.");
        return _lineStrings.Sum(x => x.GetLength());
    }

    /// <inheritdoc />
    public IPoint GetCenter()  {
        if (_lineStrings.Count == 0) throw new InvalidOperationException("This MultiLineString does not contain any LineString.");
        return MapsUtils.GetCenter(_lineStrings.SelectMany(x => x.Points));
    }

    /// <inheritdoc />
    public IRectangle GetBoundingBox() {
        if (_lineStrings.Count == 0) throw new InvalidOperationException("This MultiLineString does not contain any LineString.");
        return MapsUtils.GetBoundingBox(_lineStrings.SelectMany(x => x.Points));
    }

    /// <inheritdoc />
    public IEnumerator<ILineString> GetEnumerator() {
        return _lineStrings.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    #endregion

}