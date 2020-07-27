using Skybrud.Essentials.Maps.Geometry.Lines;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    /// <summary>
    /// Interface representing a <strong>GeoJSON</strong> line.
    /// </summary>
    public interface IGeoJsonLine : IGeoJsonGeometry {

        /// <summary>
        /// Converts the line to a corresponding instance of <see cref="ILineBase"/>.
        /// </summary>
        /// <returns>An instance of <see cref="ILineBase"/>.</returns>
        ILineBase ToLine();

    }

}