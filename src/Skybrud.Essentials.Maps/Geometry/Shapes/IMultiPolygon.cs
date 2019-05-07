using System.Collections.Generic;

namespace Skybrud.Essentials.Maps.Geometry.Shapes {

    /// <summary>
    /// Interface representing a collection of polygons.
    /// </summary>
    public interface IMultiPolygon : IShape, IEnumerable<IPolygon> {

        /// <summary>
        /// Gets an array of polygons that makes up the collection.
        /// </summary>
        IPolygon[] Polygons { get; }

    }

}