using System.Collections.Generic;

namespace Skybrud.Essentials.Maps.Geometry.Lines {

    /// <summary>
    /// Interface representing a collection of line strings.
    /// </summary>
    public interface IMultiLineString : ILineBase, IEnumerable<ILineString> { }

}