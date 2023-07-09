using System.Collections.Generic;

namespace Skybrud.Essentials.Maps.Geometry.Shapes {

    /// <summary>
    /// Interface representing a collection of polygons.
    /// </summary>
    public interface IMultiPolygon : IShape, IReadOnlyList<IPolygon> {

        /// <summary>
        /// Gets an array of polygons that makes up the collection.
        /// </summary>
        IReadOnlyList<IPolygon> Polygons { get; }

    }

}