namespace Skybrud.Essentials.Maps.Geometry.Lines {
    
    /// <summary>
    /// Interface representing a line from <see cref="A"/> to <see cref="B"/>.
    /// </summary>
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/Line_(geometry)</cref>
    /// </see>
    public interface ILine : ILineBase {

        /// <summary>
        /// Gets the starting point of the line.
        /// </summary>
        IPoint A { get; }

        /// <summary>
        /// Gets the ending point of the line.
        /// </summary>
        IPoint B { get; }

    }

}