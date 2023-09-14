namespace Skybrud.Essentials.Maps.Geometry.Shapes;

/// <summary>
/// Interface describing the common characteristics of a shape.
/// </summary>
public interface IShape : IGeometry {

    /// <summary>
    /// Returns whether this shape contains the specified <paramref name="point"/>.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns><c>true</c> if this shape contains <paramref name="point"/>; otherwise <c>false</c>.</returns>
    bool Contains(IPoint point);

    /// <summary>
    /// Returns a new point representing the center of this shape.
    /// </summary>
    /// <returns>An instance of <see cref="IPoint"/>.</returns>
    IPoint GetCenter();

    /// <summary>
    /// Returns the area of this shape, calculated in square metres.
    /// </summary>
    /// <returns>An instance of <see cref="double"/>.</returns>
    double GetArea();

    /// <summary>
    /// Returns the circumference of this shape, calculated in metres.
    /// </summary>
    /// <returns>An instance of <see cref="double"/>.</returns>
    double GetCircumference();

    /// <summary>
    /// Returns a new rectangle representing the bounding box of this shape.
    /// </summary>
    /// <returns>An instance of <see cref="IRectangle"/>.</returns>
    IRectangle GetBoundingBox();

}