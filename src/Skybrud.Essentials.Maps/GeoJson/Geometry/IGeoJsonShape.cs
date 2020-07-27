using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    /// <summary>
    /// Interface representing a <strong>GeoJSON</strong> shape.
    /// </summary>
    public interface IGeoJsonShape : IGeoJsonGeometry {

        /// <summary>
        /// Converts the shape to a corresponding instance of <see cref="IShape"/>.
        /// </summary>
        /// <returns>An instance of <see cref="IShape"/>.</returns>
        IShape ToShape();

    }

}