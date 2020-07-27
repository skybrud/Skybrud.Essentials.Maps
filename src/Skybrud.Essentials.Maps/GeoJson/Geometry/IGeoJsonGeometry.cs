using Skybrud.Essentials.Maps.Geometry;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    /// <summary>
    /// Interface representing a <strong>GeoJSON</strong> geometry.
    /// </summary>
    public interface IGeoJsonGeometry {

        /// <summary>
        /// Converts the geometry to a corresponding instance of <see cref="IGeometry"/>.
        /// </summary>
        /// <returns>An instance of <see cref="IGeometry"/>.</returns>
        IGeometry ToGeometry();

    }

}