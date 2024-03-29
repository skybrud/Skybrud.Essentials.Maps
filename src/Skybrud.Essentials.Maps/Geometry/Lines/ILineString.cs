﻿using System.Collections.Generic;

namespace Skybrud.Essentials.Maps.Geometry.Lines;

/// <summary>
/// Interface representing a line string (also referred to as a polyline).
/// </summary>
/// <see>
///     <cref>https://en.wikipedia.org/wiki/Polygonal_chain</cref>
/// </see>
public interface ILineString : ILineBase {

    /// <summary>
    /// Gets the array of points making up the line string.
    /// </summary>
    IReadOnlyList<IPoint> Points { get; }

}