using Skybrud.Essentials.Common;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.Geometry.Lines {

    /// <summary>
    /// Class representing a line between two instances of <see cref="IPoint"/>.
    /// </summary>
    public class Line : ILine {

        #region Properties

        /// <summary>
        /// Gets the start point of the line.
        /// </summary>
        public IPoint A { get; }

        /// <summary>
        /// Gets the end point of the line.
        /// </summary>
        public IPoint B { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new line based on the specified <paramref name="lat1"/>, <paramref name="lng1"/>, <paramref name="lat2"/> and <paramref name="lng2"/>.
        /// </summary>
        /// <param name="lat1">The latitude of the first point.</param>
        /// <param name="lng1">The longitude of the first point.</param>
        /// <param name="lat2">The latitude of the first point.</param>
        /// <param name="lng2">The longitude of the first point.</param>
        public Line(double lat1, double lng1, double lat2, double lng2) {
            A = new Point(lat1, lng1);
            B = new Point(lat2, lng2);
        }

        /// <summary>
        /// Initializes a new line based from <paramref name="point1"/> to <paramref name="point2"/>.
        /// </summary>
        /// <param name="point1">The first point (origin).</param>
        /// <param name="point2">The second point (destination)</param>
        public Line(IPoint point1, IPoint point2) {
            A = point1 ?? throw new PropertyNotSetException(nameof(A));
            B = point2 ?? throw new PropertyNotSetException(nameof(B));
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Returns the total length of this line.
        /// </summary>
        /// <returns>The total length in metres.</returns>
        public double GetLength() {
            return PointUtils.GetDistance(A, B);
        }
        
        /// <summary>
        /// Returns a new point representing the center of this line.
        /// </summary>
        /// <returns>An instance of <see cref="IPoint"/>.</returns>
        public IPoint GetCenter() {
            // TODO: It shouldn't be necessary to calculate the bounding box first
            return GetBoundingBox().GetCenter();
        }

        /// <summary>
        /// Returns a new rectangle representing the bounding box of this line.
        /// </summary>
        /// <returns>An instance of <see cref="IRectangle"/>.</returns>
        public IRectangle GetBoundingBox() {
            return new Rectangle(A, B);
        }

        #endregion

    }

}