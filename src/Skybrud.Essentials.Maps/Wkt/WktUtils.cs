using System;
using System.Collections.Generic;
using System.Linq;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Shapes;
using Skybrud.Essentials.Maps.Wkt.Exceptions;

namespace Skybrud.Essentials.Maps.Wkt;

/// <summary>
/// Static class with various utility/helper methods for working with <strong>Well Known Text</strong>.
/// </summary>
public static class WktUtils {

    /// <summary>
    /// Converts the specified <paramref name="point"/> to a correspoinding Well Known Text point.
    /// </summary>
    /// <param name="point">The point to be converted.</param>
    /// <returns>An instance of <see cref="WktPoint"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="point"/> is <c>null</c>.</exception>
    public static WktPoint ToWkt(IPoint point) {
        if (point == null) throw new ArgumentNullException(nameof(point));
        return new WktPoint(point);
    }

    /// <summary>
    /// Returns a two-dimensional array of coordinates from the specified <paramref name="polygon"/>.
    /// </summary>
    /// <param name="polygon">The plogyon.</param>
    /// <returns>A two-dimensional array of coordinates of <paramref name="polygon"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="polygon"/> is <c>null</c>.</exception>
    public static WktPoint[][] ToWktPoints(IPolygon polygon) {
        if (polygon == null) throw new ArgumentNullException(nameof(polygon));
        return ToWkt(MapsUtils.GetCoordinates(polygon));
    }

    /// <summary>
    /// Converts the specified <paramref name="polygon"/> to a corresponding instance of <see cref="WktPolygon"/>.
    /// </summary>
    /// <param name="polygon">The polygon to be converted.</param>
    /// <returns>An instance of <see cref="WktPolygon"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="polygon"/> is <c>null</c>.</exception>
    public static WktPolygon ToWkt(IPolygon polygon) {
        if (polygon == null) throw new ArgumentNullException(nameof(polygon));
        return new WktPolygon(polygon);
    }

    /// <summary>
    /// Converts the specified two-deimensional array of <paramref name="coordinates"/>.
    /// </summary>
    /// <param name="coordinates">The coordinates to be converted.</param>
    /// <returns>A two-dimensional array of <see cref="WktPoint"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="coordinates"/> is <c>null</c>.</exception>
    public static WktPoint[][] ToWkt(IReadOnlyList<IReadOnlyList<IPoint>> coordinates) {

        if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));

        WktPoint[][] temp = new WktPoint[coordinates.Count][];

        for (int i = 0; i < coordinates.Count; i++) {
            temp[i] = new WktPoint[coordinates[i].Count];
            for (int j = 0; j < temp[i].Length; j++) {
                temp[i][j] = new WktPoint(coordinates[i][j]);
            }
        }

        return temp;

    }

    /// <summary>
    /// Returns the area of the specified <paramref name="polygon"/> measured in sqaure metres.
    /// </summary>
    /// <param name="polygon">The polygon.</param>
    /// <returns>A <see cref="double"/> representing the area in square metres.</returns>
    /// <remarks>For this method to work, it is assumed that coordinates are specified using the
    /// <strong>WGS 84</strong> coordinate system (eg. used by the Global Positioning System).</remarks>
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/World_Geodetic_System#WGS84</cref>
    /// </see>
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/Global_Positioning_System</cref>
    /// </see>
    /// <exception cref="ArgumentNullException"><paramref name="polygon"/> is <c>null</c>.</exception>
    public static double GetArea(WktPolygon polygon) {

        if (polygon == null) throw new ArgumentNullException(nameof(polygon));

        // Get the overall area from the outer points
        double area = MapsUtils.GetArea(polygon.Outer.Select(x => new Point(x.Y, x.X)));

        // Substract the area of the inner points
        foreach (IReadOnlyList<WktPoint> inner in polygon.Inner) {
            area -= MapsUtils.GetArea(inner.Select(x => new Point(x.Y, x.X)));
        }

        return area;

    }

    /// <summary>
    /// Returns the area of the specified <paramref name="multiPolygon"/> shape measured in sqaure metres.
    /// </summary>
    /// <param name="multiPolygon">The collection of polygons.</param>
    /// <returns>A <see cref="double"/> representing the area in square metres.</returns>
    /// <remarks>For this method to work, it is assumed that coordinates are specified using the
    /// <strong>WGS 84</strong> coordinate system (eg. used by the Global Positioning System).</remarks>
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/World_Geodetic_System#WGS84</cref>
    /// </see>
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/Global_Positioning_System</cref>
    /// </see>
    /// <exception cref="ArgumentNullException"><paramref name="multiPolygon"/> is <c>null</c>.</exception>
    public static double GetArea(WktMultiPolygon multiPolygon) {
        if (multiPolygon == null) throw new ArgumentNullException(nameof(multiPolygon));
        return multiPolygon.Count == 0 ? 0 : multiPolygon.Sum(GetArea);
    }

    /// <summary>
    /// Returns the area of the specified WKT <paramref name="geometry"/> shape measured in sqaure metres.
    /// </summary>
    /// <param name="geometry">The WKT shape.</param>
    /// <returns>A <see cref="double"/> representing the area of the shape in square metres.</returns>
    /// <remarks>For this method to work, it is assumed that coordinates are specified using the
    /// <strong>WGS 84</strong> coordinate system (eg. used by the Global Positioning System).</remarks>
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/World_Geodetic_System#WGS84</cref>
    /// </see>
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/Global_Positioning_System</cref>
    /// </see>
    /// <exception cref="ArgumentNullException"><paramref name="geometry"/> is <c>null</c>.</exception>
    public static double GetArea(WktGeometry geometry) {
        if (geometry == null) throw new ArgumentNullException(nameof(geometry));
        return geometry switch {
            WktMultiPolygon multi => GetArea(multi),
            WktPolygon polygon => GetArea(polygon),
            _ => throw new InvalidOperationException("Unsupported type " + geometry.GetType())
        };
    }

    /// <summary>
    /// Converts the specified Well Known Text <paramref name="point"/> to the corresponding point in the <strong>WGS 84</strong> coordinate system.
    /// </summary>
    /// <param name="point">The Well Known Text point.</param>
    /// <returns>An instance of <see cref="IPoint"/> </returns>
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/World_Geodetic_System#WGS84</cref>
    /// </see>
    /// <exception cref="ArgumentNullException"><paramref name="point"/> is <c>null</c>.</exception>
    public static IPoint ToPoint(this WktPoint point) {
        if (point == null) throw new ArgumentNullException(nameof(point));
        return new Point(point.Y, point.X);
    }

    /// <summary>
    /// Converts the specified <paramref name="polygon"/> to a corresponding instance of <seealso cref="IPolygon"/>.
    /// </summary>
    /// <param name="polygon">The polygon to be converted.</param>
    /// <returns>An instance of <see cref="IPolygon"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="polygon"/> is <c>null</c>.</exception>
    public static IPolygon ToPolygon(WktPolygon polygon) {

        if (polygon == null) throw new ArgumentNullException(nameof(polygon));

        var outer = polygon.Outer.Select(ToPoint);
        var inner = polygon.Inner.Select(x => x.Select(ToPoint)).ToArray();

        return new Polygon(outer, inner);

    }

    /// <summary>
    /// Converts the specified <paramref name="multiPolygon"/> to a corresponding instance of <seealso cref="IMultiPolygon"/>.
    /// </summary>
    /// <param name="multiPolygon">The multi polygon to be converted.</param>
    /// <returns>An instance of <see cref="IMultiPolygon"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="multiPolygon"/> is <c>null</c>.</exception>
    public static IMultiPolygon ToMultiPolygon(WktMultiPolygon multiPolygon) {
        if (multiPolygon == null) throw new ArgumentNullException(nameof(multiPolygon));
        return new MultiPolygon(multiPolygon.Select(ToPolygon));
    }

    /// <summary>
    /// Converts the specified <paramref name="geometry"/> into a corresponding instance of <see cref="IShape"/>.
    /// </summary>
    /// <param name="geometry">The geometry to be converted.</param>
    /// <returns>An instance of <see cref="IShape"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="geometry"/> is <c>null</c>.</exception>
    /// <exception cref="WktUnsupportedTypeException">type of <paramref name="geometry"/> is not supported.</exception>
    public static IShape ToShape(WktGeometry geometry) {
        if (geometry == null) throw new ArgumentNullException(nameof(geometry));
        return geometry switch {
            WktPolygon polygon => ToPolygon(polygon),
            WktMultiPolygon multi => ToMultiPolygon(multi),
            _ => throw new WktUnsupportedTypeException(geometry.GetType().FullName!)
        };
    }

}